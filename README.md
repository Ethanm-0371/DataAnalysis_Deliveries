# Heatmap
by Jonathan Cacay, Ethan Martín, Júlia Serra and Ariadna Sevcik

Link to the repository: [https://github.com/Ethanm-0371/DataAnalysis_Deliveries/tree/third-delivery](https://github.com/Ethanm-0371/DataAnalysis_Deliveries/tree/third-delivery)
Sample Project: [https://assetstore.unity.com/packages/templates/tutorials/3d-game-kit-lite-135162](https://assetstore.unity.com/packages/templates/tutorials/3d-game-kit-lite-135162)

## Description
3D Heatmap represented in cubes in the Unity Editor for the subject "Data Analysis" to collect, analyse and visualize data of the given level.

## Table Structure
We have structured the tables in:
- Position
- Attack
- Death
- Hit
- Kill

Each table has Vector3 for each position (x, y, z).

## How to use
Each type of data appears in a dropdown with all the information related to it. Here is an example:

![positionData](https://github.com/user-attachments/assets/19c1318b-f1e7-4a83-b82b-f44b1d21878f)

In each dropdown, you will find a button to update the data, and the next variables:
### **Show Heatmap Data**
- [x] **Checkbox**.
Heatmap information will appear as cubes in different colours depending on the gradient and the data.

### **Show Raw Data**
- [x] **Checkbox**.
Raw Data will appear as spheres in gray in the original positions from the data base.

### **Gradient**
Gradient. It shows the heatmap cubes in a gradient of colours, depending on the values. As a default, the gradient is: 

![#002AFF](https://placehold.co/15x15/002AFF/002AFF.png) `#002AFF` ![#00FFE1](https://placehold.co/15x15/00FFE1/00FFE1.png) `#00FFE1` ![#15FF00](https://placehold.co/15x15/15FF00/15FF00.png) `#15FF00` ![#FFF400](https://placehold.co/15x15/FFF400/FFF400.png) `#FFF400` ![#FF0000](https://placehold.co/15x15/FF0000/FF0000.png) `#FF0000`

Gradient can be edited in the Gradient Editor. Any change on the gradient will affect only the selected data.

### Heatmap Resolution
Slider. Changes the size of the heatmap cubes to the selected resolution.

## Steps
1️⃣ Open Scene **"Visualization Scene"** and go to the first game object called **"Data Visualizer"**

2️⃣ Search for the component **"Visualization Logic (Script)"** and click the button **"Get Data"**.

![step2](https://github.com/user-attachments/assets/328616b1-5975-4b3f-a41a-88c4d8b3efaf)

> [!IMPORTANT]
> If you do not click the button "Get Data", you will not be able to see the heatmap, so be careful to not avoid this step

3️⃣ That is it! You will be able to see this set of dropdowns: 

![step3](https://github.com/user-attachments/assets/105960e7-19f8-4411-b957-ad043d3dc020)

In order to see each heatmap, remember to check the box "Show Heatmap Data" of the selected table.

> [!NOTE]
> If you want to erase any change in the heatmap, click the button "Reset all"
