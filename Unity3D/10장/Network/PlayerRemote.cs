using UnityEngine;
using System.Collections;

public class PlayerRemote : MonoBehaviour {
	
	public class State:System.Object{
		public Vector3 p;
		public Quaternion r;
		public float t;
		
		public State(Vector3 p , Quaternion r, float t)
		{
			this.p = p;
		 	this.r = r;
			this.t = t;
		}
	}
	
	public bool simulatePhysics = true;
	public bool updatePosition = true;
	public float physInterp = 0.1f;
	public float netInterp = 0.2f;
	public float ping;
	public float jitter;
	
	public bool isResponding = false;
	public string netCode = "(No Connections)";
	private int m;
	private Vector3 p;
	private Quaternion r;
	private State[] states = new State[15];
	private int stateCount = 0;
	private Vector3 velocity;
	
	private State nullstate;

	// Use this for initialization
	void Start () {
		networkView.observed = this;
		nullstate = new State(Vector3.zero,Quaternion.identity,0.0f);
		for(int i = 0 ; i < states.Length;i++)
		{
			states[i] = new State(nullstate.p,nullstate.r,nullstate.t);
		}
		velocity = new Vector3(0.0f,0.0f,0.0f);
		
	}
	
	
	void FixedUpdate(){
		if(!updatePosition || states[10].Equals(nullstate))
		{
			return;
		}
		
		float difftime = (float)Network.time - states[0].t;
		jitter = Mathf.Lerp(jitter, Mathf.Abs(ping - difftime),Time.deltaTime * 0.3f);
		ping = Mathf.Lerp(ping,(float)Network.time - states[0].t,Time.deltaTime * 0.3f);
		
		float interpolationTime = (float)Network.time - netInterp;
		if(states[0].t > interpolationTime)
		{
			int i;
			for(i = 0 ; i <stateCount;i++)
			{
				if(states[i] != null && (states[i].t <= interpolationTime || 
					i == stateCount - 1))
				{
					State rhs = states[Mathf.Max(i-1,0)];
					State lhs = states[i];
					float l = rhs.t - lhs.t;
					float t = 0.0f;
					if(l > 0.0001f)
						t = ((interpolationTime - lhs.t)/l);
					if(simulatePhysics){
						gameObject.transform.position = Vector3.Lerp(
							gameObject.transform.position,
							Vector3.Lerp(lhs.p,rhs.p,t),physInterp);
						gameObject.transform.rotation = Quaternion.Slerp(
							gameObject.transform.rotation, 
							Quaternion.Slerp(lhs.r,rhs.r,t),physInterp);						
						velocity = ((rhs.p - states[i+1].p)/(rhs.t - states[i+1].t));
						
					}else {
						gameObject.transform.position = Vector3.Lerp(lhs.p, rhs.p,t);
						gameObject.transform.rotation = Quaternion.Slerp(lhs.r,rhs.r,t);
					}
					isResponding = true;
					netCode = "";
					return;
				}
			}
		}else{
			float extrapolationLength = (interpolationTime - states[0].t);
			if(extrapolationLength < 1 && states[0].Equals(nullstate) == false 
				&& states[1].Equals(nullstate) == false){
				if(!simulatePhysics){
					gameObject.transform.position = states[0].p + 
						(((states[0].p - states[1].p) /(states[0].t - states[1].t)) * 
							extrapolationLength);
					gameObject.transform.rotation = states[0].r;
				}
				isResponding = true;
				if(extrapolationLength < 0.5f) netCode = ">";
				else netCode = "(Delayed)";
			}else{
				netCode = "(NotResponding)";
				isResponding = false;
			}
			
		}
		if(simulatePhysics && states[0].t > states[2].t)
		{
			velocity = ((states[0].p - states[2].p) /(states[0].t - states[2].t));
		}
		if(transform.localScale == Vector3.zero)
		{
			gameObject.SendMessage("MakeResizeByImageSize");
		}
	}
	void OnSerializeNetworkView(BitStream stream,NetworkMessageInfo info){
		if(stream.isWriting){
			p = gameObject.transform.position;
			r = gameObject.transform.rotation;
			m = stateCount == 0 ? 0 :(int)(((float)Network.time-states[0].t)*1000);
			stream.Serialize(ref p);
			stream.Serialize(ref r);
			stream.Serialize(ref m);
		}else{
			stream.Serialize(ref p);
			stream.Serialize(ref r);
			stream.Serialize(ref m);
			State state = new State(p,r,(float)(info.timestamp - (m > 0?(m/1000):0.0f)));
			if(stateCount == 0)
			{
				states[0] = new State(state.p,state.r,state.t);
			}else if(state.t > states[0].t)
			{
				for(int k = states.Length-1; k > 0;k--)
				{
					states[k] = states[k-1];
				}
				states[0] = new State(state.p,state.r,state.t);
				
			}else
			{
				Debug.Log(gameObject.name+"Out-of-order state received and ignored ("+
					((states[0].t-state.t)*1000)+")"+states[0].t+"---"+state.t+"---"+m+
					"---"+states[0].p.x+"---"+state.p.x);
			}
			stateCount = Mathf.Min(stateCount + 1, states.Length);
			
		}
	}
	
	public Vector3 GetVelocity()
	{
		return this.velocity;
	}
	
}

