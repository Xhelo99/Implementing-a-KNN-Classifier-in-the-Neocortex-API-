using KNN;
using NeoCortexApi.Entities;

namespace UnitTestProject
{
    public class Tests
    {

        [Test]
        public void Learn_Adds_Training_Data_Correctly()
        {
            // Arrange
            var classifier = new KnnClassifier<string, ComputeCycle>();
            var input = "label";
            var output = new Cell[] { new Cell(), new Cell(), new Cell() };

            // Act
            classifier.Learn(input, output);

            // Assert
            Assert.IsTrue(classifier.GetPredictedInputValues(output, 1).Exists(result => result.PredictedInput == input));
        }

        [Test]
        public void Classify_Returns_Correct_Prediction()
        {
            // Arrange
            var classifier = new KnnClassifier<string, ComputeCycle>();
            var input = "label";
            var output = new Cell[] { new Cell(), new Cell(), new Cell() };
            classifier.Learn(input, output);

            // Act
            var predictedLabel = classifier.Classify(output, 1);

            // Assert
            Assert.AreEqual(input, predictedLabel);
        }

        [Test]
        public void Vote_Returns_Correct_Prediction()
        {
            // Arrange
            var classifier = new KnnClassifier<string, ComputeCycle>();
            var input1 = "label1";
            var input2 = "label2";
            var output = new Cell[] { new Cell(), new Cell(), new Cell() };
            classifier.Learn(input1, output);
            classifier.Learn(input2, output);

            // Act
            var predictedLabel = classifier.Vote(output, 2);

            // Assert
            Assert.AreEqual(input1, predictedLabel);
        }

        [Test]
        public void ClearState_Clears_Training_Data()
        {
            // Arrange
            var classifier = new KnnClassifier<string, ComputeCycle>();
            var input = "label";
            var output = new Cell[] { new Cell(), new Cell(), new Cell() };
            classifier.Learn(input, output);

            // Act
            classifier.ClearState();

            // Assert
            Assert.AreEqual(0, classifier.GetPredictedInputValues(output, 1).Count);
        }
    }
}