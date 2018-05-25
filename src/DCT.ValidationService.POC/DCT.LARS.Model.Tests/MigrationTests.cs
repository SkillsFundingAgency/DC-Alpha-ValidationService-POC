using CsvHelper;
using DCT.LARS.Model.CsvMap;
using FluentAssertions;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace DCT.LARS.Model.Tests
{
    public class MigrationTests
    {
        [Fact]
        public void LoadLearningDeliveryCategories()
        {
            using (var csvReader = new CsvReader(File.OpenText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data/LearningDeliveryCategories.csv"))))
            {
                csvReader.Configuration.RegisterClassMap(typeof(LearningDeliveryCategoryMap));

                var learningDeliveryCategories = csvReader.GetRecords<LearningDeliveryCategory>().ToList();

                learningDeliveryCategories.Should().HaveCount(68979);
            }
        }
    }
}
