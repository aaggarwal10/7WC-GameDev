# 7WC - Intro to Unity3D
This is a repository designed to introduce students to the Unity 3D Platform. The following is a lesson plan made for Massey's 7 Week Challenge:

## Introduction
Unity is a great platform used for building a variety of applications. From Game to Medical Applications, Unity is a multiplatform tool that has use even in professional standards. Personally, I have used Unity for 3 years and I find that it is my preferred tool for building programming in general. This week in the 7 Week Challenge, I will be bringing you through Unity primarily through a Game Development viewpoint, but do not think that Unity can only be used in this field further down the line.

## Description of Layout
When you start up Unity, you will see a default layout something like this:

![Layout Image](../layout.jpg?raw=true)

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

![Hierarchy Image](./)

By right clicking in the Hierarchy, we can see many things that we can add like Empty Objects, 3D Objects, Lights, Cameras, etc. As we want to create a cube, we will hover over 3D Object, and select a cube now. We have a cube in the scene. However, we might want to navigate around the scene to better see the cube. 

![Controls]()

At the top left, there is a task bar with different settings that will allow you to do different actions in the scene:
* **Hand Tool:** Used to pan (rotate on right click) in scene to move viewpoint (shortcut: middle mouse button)
* **Move Tool:** Allows the selection of objects and the changing of an objects position in the scene (Most Used Probably)
* **Rotate Tool:** Rotation of object in scene
* **Scale Tool:**  Scaling of object (Shrink, Expand)
* **Rect Tool:** 2D changes mainly in UI.

Furthermore, one can go forward and backward in the scene by using the scroll wheel.

Now, let us take a look at the inspector. Double click on the cube in the scene. It will bring you to a closer view of the cube in the scene (in case you lost it). On the right side, you should also see the details of the cube in the inspector. You can try using the scale / rotate tool or you can try changing the transform properties of the object.

![Box Collider]()

You should also see the box collider component on the cube. A cube automatically has a box collider to deal with physics. Before we start making our cube move, let us start with the "groundwork." We will first make a plane to act as ground so that we can give our cube gravity so that it does not just float through the air. This is simple in Unity, by making a plane below the cube in the hierarchy, and then adding a rigid body component to the cube while turning on gravity. Congratulations, you have successfully created gravity in Unity. 

![Update Project]()

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
        if (Input.GetKey(KeyCode.W)) // Key Down Clicks
        {
            change_z = 1;
        } else if (Input.GetKey(KeyCode.S))
        {
            change_z = -1;
        } else if (Input.GetKey(KeyCode.A))
        {
            change_x = -1;
        } else if (Input.GetKey(KeyCode.D))
        {
            change_x = 1;
        }
        Vector3 change = new Vector3(change_x, 0, change_z);
        this.transform.position = this.transform.position + speed * change;
    }
}
```