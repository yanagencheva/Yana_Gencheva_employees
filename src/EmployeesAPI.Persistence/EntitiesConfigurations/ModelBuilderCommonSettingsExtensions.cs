using Microsoft.EntityFrameworkCore;

namespace EmployeesAPI.Persistence.EntitiesConfigurations;

public static class ModelBuilderCommonSettingsExtensions
{
    public static void ApplyCommonTypeSettings(this ModelBuilder modelBuilder)
    {
        var allEntityTypes = modelBuilder.Model.GetEntityTypes();

        var allModelsProperties = allEntityTypes.SelectMany(t => t.GetProperties()).ToArray();
        foreach (var property in allModelsProperties)
        {
            property.IsNullable = false;
        }

        foreach (var property in allModelsProperties.Where(p => p.ClrType == typeof(int)))
        {
            property.SetColumnType($"int8");
        }

        foreach (var property in allModelsProperties.Where(p => p.ClrType == typeof(string)))
        {
            property.SetColumnType($"character varying(100)");
        }
    }
}
