using System;
using UnityEngine;

public class NewMovement : MonoBehaviour
{
    [Serializable]
    public class MovementSettings
    {
        public float ForwardSpeed = 8.0f;   // წინ სიარულის სიჩქარე
        public float BackwardSpeed = 4.0f;  // უკან სიარულის სიჩქარე
        public float StrafeSpeed = 4.0f;    // გვერდებზე სიარულის სიჩქარე
        public float RunMultiplier = 2.0f;   // სირბილის სიჩქარე
        public KeyCode RunKey = KeyCode.LeftShift;
        [HideInInspector] public float CurrentTargetSpeed = 8f;

        public void UpdateTargetSpeed(Vector2 input)
        {
            if (input == Vector2.zero) return;
            if (input.x > 0 || input.x <0)
            {
                //თუ input გვაძლევს ჰორიზონტალურ გადაადგილებას, რაც მოწმდება if-ში, მოძრაობის სიჩქარე უტოლდება გვერდებზე მოძრაობის სიჩქარეს
                CurrentTargetSpeed = StrafeSpeed;
            }
            if (input.y < 0)
            {
                //თუ input გვაძლევს ვერტიკალურ გადაადგილებას, და ეს გადაადგილება უარყოფითია (ანუ უკან მიდის), მოძრაობის სიჩქარე უტოლდება უკან სიარულის სიჩქარეს
                CurrentTargetSpeed = BackwardSpeed;
            }
            if (input.y > 0)
            {
                //თუ ვერტიკალურად მოძრაობა დადებითია, მოძრაობის სიჩქარე უტოლდება წინ სიარულის სიჩქარეს
                //და კიდევ, ბოლოს იმიტომ წერია, რომ კოდი მიმდევრობით მოწმდება, ამიტომ წინ და გვერდზეც როცა მიდის, პერსონაჟი შეინარჩუნებს წინ სიარულის სიჩქარეს და არ გადავა გვერდზე სიარულის სიჩქარეზე
                CurrentTargetSpeed = ForwardSpeed;
            }

            //სირბილი
            if (Input.GetKey(RunKey))
                CurrentTargetSpeed *= RunMultiplier;
        }
    }

    public Camera cam;
    public MovementSettings movementSettings = new MovementSettings();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 input = GetInput();
    }

    private Vector2 GetInput()
    {
        //ამას თქვენც მიხვდებით, იღებს Horizontal და Vertical მნიშვნელობებს და გადასცემს UpdateTargetSpeed ფუნქციას(თუ მეთოდს)
        Vector2 input = new Vector2
        {
            x = Input.GetAxis("Horizontal"),
            y = Input.GetAxis("Vertical")
        };
        movementSettings.UpdateTargetSpeed(input);
        return input;
    }
}
