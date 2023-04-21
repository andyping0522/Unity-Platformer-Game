
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script requires you to have setup your animator with 3 parameters, "InputMagnitude", "InputX", "InputZ"
//With a blend tree to control the inputmagnitude and allow blending between animations.
// animation and some of the strucuture code are been given from the asset as an package
[RequireComponent(typeof(CharacterController))]
public class MovementInput : MonoBehaviour {

    public float Velocity;
    [Space]

	public float InputX;
	public float InputZ;
	//2d movement variables
	public Vector3 desiredMoveDirection;
	public bool blockRotationPlayer;
	public float desiredRotationSpeed = 0.1f;
	public Animator anim;
	public float Speed;
	public float allowPlayerRotation = 0.1f;
	public Camera cam;
	public CharacterController controller;
	public bool isGrounded;
	// jumping variables
	public float gravity_scale;
	public int jumps;
	public int jumpvalue;
	private float jumptime;
	public float jumptimevalue;
	public bool jumping;
	private bool wallJump;
	private bool sliding;
	
	private bool boost;
	public float physic = Physics.gravity.y;
	public float boostForce;

	// dashing variables 
	public float dash_velocity;
	private float dashtime;
	public float dashtimevalue;
	private float dashdowntime;
	public float dashdowntimevalue;
	public GameObject dashparticle;

	// Death flag
	private bool deathFlag;


    [Header("Animation Smoothing")]
    [Range(0, 1f)]
    public float HorizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f;
    [Range(0,1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;

    public float verticalVel;
	public float jumpForce = 5;
	public float gravity = 9.81f;
    private Vector3 moveVector;
	private Vector3 hitNormal;

	// health
	public int health;
	public static int death = 0;
	private bool canMove = true;

	private IEnumerator WaitForSceneLoad()
	{
		yield return new WaitForSeconds(3);
		Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
		death += 1;
		Score.score = 0;
	}
	public void HurtPlayer(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
			SetDeath();
            canMove = false;
            anim.SetTrigger("die");
			StartCoroutine(WaitForSceneLoad());
			// Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }
    }
	
	
	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator> ();
		cam = Camera.main;
		controller = this.GetComponent<CharacterController> ();
		jumps = jumpvalue;
		jumptime = jumptimevalue;
		
		boost = false;
		dashtime = dashtimevalue;
		deathFlag = false;
		// DontDestroyOnLoad(this);

	}
	
	// Update is called once per frame
	// fixedupdate handles non jumping movement as well as animation
	void FixedUpdate () {
		if (canMove)
		{
			InputMagnitude ();

			var camera = Camera.main;
			var forward = cam.transform.forward;
			var right = cam.transform.right;
		
			forward.y = 0f;
			right.y = 0f;

			forward.Normalize ();
			right.Normalize ();
			desiredMoveDirection = right * InputX;
			Speed = new Vector2(InputX, 0).sqrMagnitude;

			if (blockRotationPlayer == false && Speed > allowPlayerRotation) {
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (desiredMoveDirection), desiredRotationSpeed);
			}

			InputX = Input.GetAxis ("Horizontal");
			InputZ = Input.GetAxis ("Vertical");

			moveVector = new Vector3(InputX * Velocity, moveVector.y, 0);
			// key input for dashing as well as particle effect for 
			if(dashtime > 0 && InputX > 0 && Input.GetKey(KeyCode.LeftShift)){
				moveVector = new Vector3(moveVector.x * Velocity+ dash_velocity, moveVector.y, 0);
				GameObject effect = (GameObject)Instantiate(dashparticle, transform.position +new Vector3(moveVector.x* Time.deltaTime +1, 0.5f,0)  ,new Quaternion(transform.rotation.x, -transform.rotation.y, transform.rotation.z,transform.rotation.w));
				dashtime = dashtime - 1;

			}
			else if(dashtime > 0 && InputX < 0 && Input.GetKey(KeyCode.LeftShift)){
				moveVector = new Vector3(moveVector.x * Velocity - dash_velocity, moveVector.y, 0);
				GameObject effect = (GameObject)Instantiate(dashparticle, transform.position +new Vector3(moveVector.x* Time.deltaTime - 1, 0.5f,0)  ,new Quaternion(transform.rotation.x, -transform.rotation.y, transform.rotation.z,transform.rotation.w));
				dashtime = dashtime - 1;

			}
			

			if (dashtime <=0){
				dashdowntime = dashdowntime - Time.deltaTime;
			}

			if (!Physics.Raycast(this.transform.position, -hitNormal, 0.8f)) {
				// end sliding
				wallJump = false;
				sliding = false;
			}

			
			if (boost) {
				moveVector.y = boostForce;
				boost = false;
			}
			
			controller.Move(moveVector * Time.deltaTime);
			// InputMagnitude ();
		}
		// InputMagnitude ();

		// var camera = Camera.main;
		// var forward = cam.transform.forward;
		// var right = cam.transform.right;
	
		// forward.y = 0f;
		// right.y = 0f;

		// forward.Normalize ();
		// right.Normalize ();
		// desiredMoveDirection = forward * InputZ + right * InputX;
		// Speed = new Vector2(InputX, InputZ).sqrMagnitude;

		// if (blockRotationPlayer == false && Speed > allowPlayerRotation) {
		// 	transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (desiredMoveDirection), desiredRotationSpeed);
		// }

		// InputX = Input.GetAxis ("Horizontal");
		// InputZ = Input.GetAxis ("Vertical");

		// moveVector = new Vector3(InputX * Velocity, moveVector.y, InputZ * Velocity);
		// // key input for dashing as well as particle effect for 
		// if(dashtime > 0 && InputX > 0 && Input.GetKey(KeyCode.LeftShift)){
		// 	moveVector = new Vector3(moveVector.x * Velocity+ dash_velocity, moveVector.y, moveVector.z * Velocity);
		// 	GameObject effect = (GameObject)Instantiate(dashparticle, transform.position +new Vector3(moveVector.x* Time.deltaTime +1, 0.5f,0)  ,new Quaternion(transform.rotation.x, -transform.rotation.y, transform.rotation.z,transform.rotation.w));
		// 	dashtime = dashtime - 1;

		// }
		// else if(dashtime > 0 && InputX < 0 && Input.GetKey(KeyCode.LeftShift)){
		// 	moveVector = new Vector3(moveVector.x * Velocity - dash_velocity, moveVector.y, moveVector.z * Velocity);
		// 	GameObject effect = (GameObject)Instantiate(dashparticle, transform.position +new Vector3(moveVector.x* Time.deltaTime - 1, 0.5f,0)  ,new Quaternion(transform.rotation.x, -transform.rotation.y, transform.rotation.z,transform.rotation.w));
		// 	dashtime = dashtime - 1;

		// }
		

		// if (dashtime <=0){
		// 	dashdowntime = dashdowntime - Time.deltaTime;
		// }

		// if (!Physics.Raycast(this.transform.position, -hitNormal, 0.8f)) {
		// 	// end sliding
		// 	wallJump = false;
		// 	sliding = false;
		// }

		
		// if (boost) {
		// 	moveVector.y = boostForce;
		// 	boost = false;
		// }
        
		// controller.Move(moveVector * Time.deltaTime);



    }

	public void SetDeath() {
		deathFlag = true;
	}

	public bool getDeathFlag() {
		return deathFlag;
	}

	// update handles jumping movement
	void Update(){
		if (canMove){
			if (this.transform.position.y < -1) {
				HurtPlayer(1);
			}
			isGrounded = controller.isGrounded;
			moveVector = new Vector3(0, moveVector.y, 0);
			if (isGrounded){
				jumps = jumpvalue;
				jumping = true;
				jumptime = jumptimevalue;
				wallJump = false;
				
				sliding = false;
				moveVector.y = 0;
				if(dashdowntime <=0){
				dashtime = dashtimevalue;
				dashdowntime = dashdowntimevalue;
				}
			}
			if(Input.GetKeyDown(KeyCode.Space) && isGrounded == true){
				moveVector.y = 4;
				jumps = jumps -1;
			}
			else if(Input.GetKeyDown(KeyCode.Space) && jumps > 0 && !wallJump){

				moveVector.y = 6;
				jumps = jumps -1;
			}
			else if (Input.GetKey(KeyCode.Space) && isGrounded == false && jumping == true){
				// the variable jumping aliment is inspired by tutorial https://www.youtube.com/watch?v=j111eKN8sJw&t=23s
				if(jumptime > 0){
					moveVector.y = 4;
					jumptime = jumptime - Time.deltaTime;
				}
				else{	
					jumping = false;
				}

			} else if (!isGrounded) {
				if (Input.GetKeyDown(KeyCode.Space) && wallJump) {
					//Debug.Log("bruh");
					wallJump = false;
					
					moveVector.y = 7;
					jumps = jumpvalue -1;
					//moveVector = moveVector * hitNormal;
					sliding = false;
				} else if (sliding && moveVector.y < 0) {
					moveVector.y = -1;
				}
				
			}

			if(Input.GetKeyUp(KeyCode.Space)){
				jumping = false;
			}
			
			moveVector.y = moveVector.y + Physics.gravity.y * gravity_scale * Time.deltaTime;
			controller.Move(moveVector * Time.deltaTime);
		}

	}

	// lookat and rotatetoCamera functions re currently unused
    public void LookAt(Vector3 pos)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pos), desiredRotationSpeed);
    }

    public void RotateToCamera(Transform t)
    {

        var camera = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        desiredMoveDirection = forward;

        t.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
    }

	void InputMagnitude() {
		//Calculate Input Vectors
		InputX = Input.GetAxis ("Horizontal");
		InputZ = Input.GetAxis ("Vertical");

	

		//anim.SetFloat ("InputZ", InputZ, VerticalAnimTime, Time.deltaTime * 2f);
		//anim.SetFloat ("InputX", InputX, HorizontalAnimSmoothTime, Time.deltaTime * 2f);

		//Calculate the Input Magnitude


        //Physically move player

		anim.SetFloat ("Blend", Speed, StopAnimTime, Time.deltaTime);
		
	}

	private void OnControllerColliderHit(ControllerColliderHit hit) {
		isGrounded = controller.isGrounded;
		
		if (!controller.isGrounded && hit.gameObject.tag == "Wall") {
			hitNormal = hit.normal;
			
			wallJump = true;
			
			if (hit.normal.y < -0.5) {
				return;
			}
			sliding = true;
		}
		
		if (hit.gameObject.tag == "JumpBooster" && isGrounded) {
			Debug.Log("Boost!");
			boost = true;
			
		}

	}
}
