# ML22/23-2	Investigate and Implement KNN Classifier


## Feature overview

*   [x] **KNN Classifier implemented in C#** 
*   [x] **KNN Classifier integrated with HTM**
## Contents

*   [What is this?](#what-is-this)
*   [Getting started](#getting-started)
    *   [Install](#install)
*  [Integration of Classifiers with Neocortex API](#integration-of-classifiers-with-neocortex-api)
*  [Usage](#usage)
*  [Conclusion](#conclusion)
*  [Sources](#sources)
        
## What is this?

This code presents a KNN classifier implemented in C#, further enhanced by integrating it with the Neocortex API. The SE project used a sequence of values with preassigned labels to train the model. Once the model is trained, users can provide an unclassified/test sequence that needs to be labeled. We used two sequences to train the model:

- `("S1", new List<double>(new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0 }))`
- `("S2", new List<double>(new double[] { 10.0, 11.0, 12.0, 13.0, 14.0, 15.0, 16.0 }))`

## Getting Started

To set up the necessary components for this project, please follow the instructions provided below.

### Install

To get started, make sure you have the .NET framework installed on your system.
- **The latest version of .NET is 8.0**: Refer to the [Install .NET on Windows Guide](https://learn.microsoft.com/en-us/dotnet/core/install/windows?tabs=net80) for detailed instructions on setting up .NET.

Then add the NeoCortexApi NuGet package: 
 ```bash
dotnet add package NeoCortexAPI version 1.1.5
```

For managing NuGet packages. 
- **NuGet packages**: Refer to the [Install and manage NuGet packages](https://learn.microsoft.com/en-us/nuget/consume-packages/install-use-packages-dotnet-cli) to easily install, uninstall, and update NuGet packages in .NET projects and solutions.

## Integration of Classifiers with Neocortex API
The figure below shows the hierarchical structure of HTM-CLA. The basic unit of hierarchy is the cell in HTM-CLA. The cells are organized in columns. These columns combine to form a region and regions combine to form hierarchies. The cells can be connected with other cells in the same region or regions at upper or lower levels. The cells learn and store the temporal sequence of the data while columns denote the semantics of the data through SDR representations. The classifier tries to infer the output from active columns in the upper region in the hierarchy.  The old knnClassifier maps the nearest neighbors and infers the output class. However, it requires every data point to be stored in memory which results in very high complexity for huge datasets.   

![Image](Documentation/images/HtmPipeline.png)

To better understand HTM CLA refer to the [NeoCortext documentation](https://github.com/ddobric/neocortexapi/blob/master/source/Documentation/gettingStarted.md)

## Usage

**Program.cs** and **MultiSequenceLearning.cs** classes are already implemented in Neocortex API.
- **TemporalMemory**: Refer to the [How to use TemporalMemory algorithm](https://github.com/ddobric/neocortexapi/blob/master/source/Documentation/TemporalMemory.md) for detailed instructions on setting up HTM.

We have implemented [KnnClassifier.cs](https://github.com/Xhelo99/Implementing-a-KNN-Classifier-in-the-Neocortex-API-/blob/main/MyProject/KnnClassifier.cs), which can be found at the following location in this repository: 
 ```bash
MyProject/KnnClassifier.cs
```
In order to integrate the **KnnClassifier.cs** with HTM, in **MultiSequenceLearning.cs** class you replace: 
```bash
HtmClassifier<string, ComputeCycle> cls = new HtmClassifier<string, ComputeCycle>();
```
with the following line:
```bash
 KnnClassifier<string, ComputeCycle> cls = new KnnClassifier<string, ComputeCycle>();
```
The next step is to modify the [Predictor.cs](https://github.com/ddobric/neocortexapi/blob/master/source/NeoCortexApi/Predictor.cs), you have to make two changes in this class. Replace these two lines: 
```bash
private HtmClassifier<string, ComputeCycle> classifier { get; set; };
public Predictor(CortexLayer<object, object> layer, Connections connections, HtmClassifier<string, ComputeCycle> classifier)
```
With the following lines: 
```bash
private KnnClassifier<string, ComputeCycle> classifier { get; set; }
public Predictor(CortexLayer<object, object> layer, Connections connections, KnnClassifier<string, ComputeCycle> classifier)
```
After following the instructions above, you have an integrated KnnClassifier with HTM in [NeocortexApi](https://github.com/ddobric/neocortexapi).

## Conclusion:  

The K-Nearest Neighbors (KNN) model has been seamlessly integrated with the Neocortex API, allowing it to process data streams and make predictions efficiently. Through this integration, the model receives data sequences and accurately classifies them as either matching or mismatching the input sequence. 

## Sources
1. [NeoCortext API](https://github.com/ddobric/neocortexapi)
2. [University implementation](https://github.com/UniversityOfAppliedSciencesFrankfurt/LearningApi/blob/f713a28984e8f3115952c54cd9d60d53faa76ffe/LearningApi/src/MLAlgorithms/AnomDetect.KMeans/KMeansAlgorithm.cs)
3. [HTM School](https://www.youtube.com/playlist?list=PL3yXMgtrZmDqhsFQzwUC9V8MeeVOQ7eZ9&app=desktop)
4. [K-Nearest Neighbor](https://medium.com/swlh/k-nearest-neighbor-ca2593d7a3c4)














