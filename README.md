# 7WC - Intro to Unity3D
This is a repository designed to introduce students to the Unity 3D Platform. The following is a lesson plan made for Massey's 7 Week Challenge:

## Introduction
Unity is a great platform used for building a variety of applications. From Game to Medical Applications, Unity is a multiplatform tool that has use even in professional standards. Personally, I have used Unity for 3 years and I find that it is my preferred tool for building programming in general. This week in the 7 Week Challenge, I will be bringing you through Unity primarily through a Game Development viewpoint, but do not think that Unity can only be used in this field further down the line.

## Description of Layout
When you start up Unity, you will see a default layout something like this:

![Layout Image](https://github.com/aaggarwal10/7WC-GameDev/blob/main/Images/layout.png?raw=true)

The Unity Layout is separated into many sections with different purposes. For this tutorial we will look at the basic sections:
* **Project:** Assets and Models are stored (Much like a file manager)
* **Console:** Output from run time is displayed here (including warnings/errors)
* **Scene:** The 3D world containing all objects and the environment in the game
* **Game:** The Environment visible through cameras (What the player will see when they run the game)
* **Hierarchy:** Stores references and structure of all objects in the scene
* **Inspector:** Panel that allows developers to inspect and alter the properties of objects in the scene and project

Any Unity developer will find themselves interacting with these sections for any Unity project. Now to get familiar with these sections the best way is through practice. 

## Lesson 1: Creating a Moving Cube
To make a moving cube we first need to create a cube. The way to create basic objects is through the *Hierarchy* section.

![Hierarchy Image](https://github.com/aaggarwal10/7WC-GameDev/blob/main/Images/hierarchy.png?raw=true)

By right clicking in the Hierarchy, we can see many things that we can add like Empty Objects, 3D Objects, Lights, Cameras, etc. As we want to create a cube, we will hover over 3D Object, and select a cube now. We have a cube in the scene. However, we might want to navigate around the scene to better see the cube. 

![Controls](https://github.com/aaggarwal10/7WC-GameDev/blob/main/Images/controls.png?raw=true)

At the top left, there is a task bar with different settings that will allow you to do different actions in the scene:
* **Hand Tool:** Used to pan (rotate on right click) in scene to move viewpoint (shortcut: middle mouse button)
* **Move Tool:** Allows the selection of objects and the changing of an objects position in the scene (Most Used Probably)
* **Rotate Tool:** Rotation of object in scene
* **Scale Tool:**  Scaling of object (Shrink, Expand)
* **Rect Tool:** 2D changes mainly in UI.

Furthermore, one can go forward and backward in the scene by using the scroll wheel.

Now, let us take a look at the inspector. Double click on the cube in the scene. It will bring you to a closer view of the cube in the scene (in case you lost it). On the right side, you should also see the details of the cube in the inspector. You can try using the scale / rotate tool or you can try changing the transform properties of the object.

![Box Collider](https://github.com/aaggarwal10/7WC-GameDev/blob/main/Images/box.png?raw=true)
You should also see the box collider component on the cube. A cube automatically has a box collider to deal with physics. Before we start making our cube move, let us start with the "groundwork." We will first make a plane to act as ground so that we can give our cube gravity so that it does not just float through the air. This is simple in Unity, by making a plane below the cube in the hierarchy, and then adding a rigid body component to the cube while turning on gravity. Congratulations, you have successfully created gravity in Unity. 

![Update Project](https://github.com/aaggarwal10/7WC-GameDev/blob/main/Images/material.png?raw=true)
We can know add colour to our monotonic world by creating materials in our project folder. Specifically, right clicking in a the project section will allow you to *Create > Material*. Then, you can change the colour properties in the inspector and drag these materials onto objects into the scene to give those objects that specific colour property.

Now all that is left is to make our cube move. In order to do this, we need to use **Scripting**. We will start creating a C# script (Unity's scripting language), by making a new script in the project. Again, *Right Click > Create > C# Script*. Name the script and open it in Visual Studios or another editor of your choice.

From this script, you will see the two basic functions of Unity scripting. Start is called on the frame when a script is enabled just before any of the Update methods are called the first time. Update, on the other hand, is called once each frame. There are other built in runtime functions, but for now we will focus on these two.

We will first start with getting input on each frame (so in update function). Using the following code, we can get our Cube to move via WASD keys.  

```C#
public class Movement : MonoBehaviour
{
    
    int change_x, change_z; // get changes in both directional axis.
    public float speed; // Adjust speed in editor

    // Start is called before the first frame update
    void Start()
    {
        change_x = 0;
        change_z = 0;
    }

    // Update is called once per frame
    void Update()
    {
        change_x = 0;
        change_z = 0;
        if (Input.GetKey(KeyCode.W)) // Key Down Event
        {
            change_z = 1;
        } else if (Input.GetKey(KeyCode.S))
        {
            change_z = -1;
        } else if (Input.GetKey(KeyCode.D))
        {
            change_x = -1;
        } else if (Input.GetKey(KeyCode.A))
        {
            change_x = 1;
        }
        Vector3 change = new Vector3(change_x, 0, change_z);
        this.transform.position = this.transform.position + speed * change; // Linear Movement
    }
}
```

Now, we will move onto to trying to make this a first player controller. To do this we need to look at the **Camera** in the scene. The Camera provides the viewport for the player which means that what the camera sees is what the player sees in the world when they play the game. Thus, to do this we will attach the Camera to the cube in the hierarchy. This is called parenting an object. Simply put the Cube becomes the *parent* of the Camera while the Camera becomes a *child* of the Cube. This means that the transforms of the Cube will affect the Camera transform as well. Hence when our camera now follows our cube. We can add a simple rotation script with our mouse to allow for rotation of our cube and finally complete the first person cube movement. To make things easier, we will make a Camera Positioner Object that will simply alter the camera position (not orientation). This will make it easy to change POVs while allowing humanoid animations to not cause camera jitter.
First we will edit the movement script to allow a rotation axis. 

```C#
public class Movement : MonoBehaviour
{

    int change_x, change_z; // get changes in both directional axis.
    public float speed; // Adjust speed in editor
    public float speedH = 2.0f;
    public float horizRot = 0f;
    // Start is called before the first frame update
    void Start()
    {
        change_x = 0;
        change_z = 0;
    }

    // Update is called once per frame
    void Update()
    {
        change_x = 0;
        change_z = 0;
        if (Input.GetKey(KeyCode.W)) // Key Down Event
        {
            change_z = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            change_z = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            change_x = -1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            change_x = 1;
        }
        
        horizRot += speedH * Input.GetAxis("Mouse X"); //  Rotation Usin Horizontal Mouse Position
        Vector3 forw = transform.forward.normalized; // Forward from Camera Position
        Vector3 right = Vector3.Cross(forw, transform.up); // Gets the Right Strafe Vector
        Vector3 change = change_z * forw + change_x * right;

        Vector3 rot = transform.eulerAngles;
        transform.eulerAngles = new Vector3(rot.x, horizRot, 0); 
        this.transform.position = this.transform.position + speed * change; // Linear Movement
    }
}
```

Now to Deal with our rotation Up and Down, we will add a separate script just to the camera as we want the camera to change orientation up and down not the entire model.

```C#
public class CameraController : MonoBehaviour
{
    public float speed;
    public float min;
    public float max;
    public GameObject positioner;

    // Update is called once per frame
    void Update()
    {
        float camRot = Input.GetAxis("Mouse Y") * speed;
        Vector3 origRot = transform.eulerAngles;
        transform.Rotate(-camRot, 0, 0);

        float rot = transform.eulerAngles.x;
        if (rot > 180)
        {
            rot -= 360;
        }
        if (min > rot || max < rot)
        {
            transform.eulerAngles = origRot;
        }
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        transform.position = positioner.transform.position;
    }
}
```
Now, our Moving Box is Complete.

## Lesson 2: Humanoid Player & Wall Destroyer Game
![Animation Controller](https://github.com/aaggarwal10/7WC-GameDev/blob/main/Images/controller.png?raw=true)
![Animation Tree](https://github.com/aaggarwal10/7WC-GameDev/blob/main/Images/tree.png?raw=true)
Here, we first will take our model and have Unity perform it Rigging on it along with making an avatar for it by using the inspector. Then, we will have to set up our Animation Controller to animate the figure based on certain parameters. For us, we will have the parameters ChangeX, ChangeY, and Punch. The ChangeX and ChangeY components we will make part of a Blend Tree for motion while the Punch will be controlled by a separate parameter that will tell the controller when to animate the punch or to walk. This is simply a set up of an animation controller which can be done using a 2D Blending System. I go over this in detail in the Workshop. Watch the recording for the specifics.

After, we have the Animation Controller Set Up, we will have to move on to editing our previous Movement Script to know edit the parameters in the animator. 
```C#
public class PlayerController : MonoBehaviour
{
    Animator animator;
    
    int change_x, change_z;
    public float speed;
    public float speedH = 2.0f;
    float horizRot = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        change_x = 0;
        change_z = 0;
    }

    // Update is called once per frame
    void Update()
    {
        change_x = 0;
        change_z = 0;
        Debug.Log("B " + animator.GetCurrentAnimatorStateInfo(0).IsName("Punch"));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Punching", true);
        }
        else
        {
            animator.SetBool("Punching", false);
            if (Input.GetKey(KeyCode.W))
            {
                change_z = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                change_z = -1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                change_x = -1;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                change_x = 1;
            }
            horizRot += speedH * Input.GetAxis("Mouse X");
            Vector3 forw = transform.forward.normalized;
            Vector3 right = Vector3.Cross(forw, transform.up);
            Vector3 change = change_z * forw + change_x * right;
            this.transform.position = this.transform.position + speed * change;

            Vector3 rot = transform.eulerAngles;
            transform.eulerAngles = new Vector3(rot.x, horizRot, 0);
        }
        animator.SetFloat("ChangeY", change_z);
        animator.SetFloat("ChangeX", -change_x);
    }
}
```
![Trigger Component](https://github.com/aaggarwal10/7WC-GameDev/blob/main/Images/trigger.png?raw=true)

Now, our movement of the character should be complete. To add the wall breaking component, we need to look at the collisions between colliders. If you recall with the rigid body before we used Unity's inbuilt engine to work out the collisions for us. However, in some cases, we do not want to actually have collision physics, but instead only want to check if two objects collide. We can do this through a OnTrigger Script. Simply, what an OnTrigger event does is it checks when a Trigger object collides with a Non-Trigger Object. Using an on Trigger Exit we can simply look for collision through the script:
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestroy : MonoBehaviour
{
    public Animator animator;
    private void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Wall" && animator.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
        {
            Destroy(coll.gameObject);
            Debug.Log(coll.name);
        }
    }
}
```
Note: To put this script on the humanoid character, we want to put it on the hand collider object, which will be in the rigged bone hierarchy. With all of this complete we will have our Wall Breaking Project Complete. 

For the second part of the Lesson, I did not go through the specific Unity editor parts like rigging and animating your humanoid controller. It might differ depending on the model you use. For our purposes, I have gone through this in the Workshop so if you are having difficulty with it, I highly recommend going through the Workshop Video for the in editor portions. 

## Resources
* **[Unity Documentation](https://docs.unity3d.com/Manual/index.html)**: Unity has a great amount of documentation that should be referenced when looking for syntax or help on specific problems.
* **[Unity Asset Store](https://assetstore.unity.com/)**: Unity has an amazing asset store with tons of free assets that can be used to get free models and animations for your game.