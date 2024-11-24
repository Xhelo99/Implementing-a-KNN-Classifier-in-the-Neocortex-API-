using KNN;
using NeoCortexApi.Entities;

namespace UnitTestProject
{
    public class Tests
    {

        private KnnClassifier<string, ComputeCycle> knnClassifier;

        [TestInitialize]
        public void TestInitialize()
        {
            knnClassifier = new KnnClassifier<string, ComputeCycle>();
        }

        [TestMethod]
        public void TestLearnAndClassify()
        {
            // Arrange
            var trainingInput = "A";
            var trainingOutput = new[] { new Cell { Index = 1 }, new Cell { Index = 2 } };
            knnClassifier.Learn(trainingInput, trainingOutput);

            var testCells = new[] { new Cell { Index = 1 }, new Cell { Index = 2 } };

            // Act
            var result = knnClassifier.Classify(testCells);

            // Assert
            Assert.AreEqual("A", result);
        }

        [TestMethod]
        public void TestGetPredictedInputValues()
        {
            // Arrange
            knnClassifier.Learn("A", new[] { new Cell { Index = 1 }, new Cell { Index = 2 } });
            knnClassifier.Learn("B", new[] { new Cell { Index = 2 }, new Cell { Index = 3 } });

            var testCells = new[] { new Cell { Index = 1 }, new Cell { Index = 2 } };

            // Act
            var predictions = knnClassifier.GetPredictedInputValues(testCells, 2);

            // Assert
            Assert.AreEqual(2, predictions.Count);
            Assert.AreEqual("A", predictions[0].PredictedInput);
        }

        [TestMethod]
        public void TestVote()
        {
            // Arrange
            knnClassifier.Learn("A", new[] { new Cell { Index = 1 }, new Cell { Index = 2 } });
            knnClassifier.Learn("B", new[] { new Cell { Index = 2 }, new Cell { Index = 3 } });
            knnClassifier.Learn("A", new[] { new Cell { Index = 1 }, new Cell { Index = 1 } });

            var testCells = new[] { new Cell { Index = 1 }, new Cell { Index = 2 } };

            // Act
            var result = knnClassifier.Vote(testCells, 3);

            // Assert
            Assert.AreEqual("A", result);
        }

        [TestMethod]
        public void TestClearState()
        {
            // Arrange
            knnClassifier.Learn("A", new[] { new Cell { Index = 1 }, new Cell { Index = 2 } });

            // Act
            knnClassifier.ClearState();
            var testCells = new[] { new Cell { Index = 1 }, new Cell { Index = 2 } };
            var result = knnClassifier.Classify(testCells);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestNoPredictionWhenEmpty()
        {
            // Arrange
            var testCells = new[] { new Cell { Index = 1 }, new Cell { Index = 2 } };

            // Act
            var result = knnClassifier.Classify(testCells);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestDistanceCalculation()
        {
            // Arrange
            knnClassifier.Learn("A", new[] { new Cell { Index = 1 }, new Cell { Index = 2 } });
            var testCells = new[] { new Cell { Index = 3 }, new Cell { Index = 4 } };

            // Act
            var predictions = knnClassifier.GetPredictedInputValues(testCells, 1);

            // Assert
            Assert.IsTrue(predictions[0].Similarity > 0); // Ensure some similarity
        }
    
}
}