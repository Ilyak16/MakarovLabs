using FactoryApp.Class;
using FactoryApp.Interface;

public static class ShapeFactoryProvider
{
    public static IShapeFactory GetFactory(string theme)
    {
        return theme switch
        {
            "Красный" => new RedFactory(),
            "Синий" => new BlueFactory(),
            "Зелёный" => new GreenFactory(),
            _ => new RedFactory()
        };
    }
}