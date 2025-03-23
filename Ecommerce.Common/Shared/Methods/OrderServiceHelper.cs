using System;

namespace Ecommerce.Common.Shared.Methods;

public static class OrderServiceHelper
{
    public static string GenerateOrderReference()
    {
        return $"ORD-{Guid.NewGuid().ToString("N").Substring(0, 12).ToUpper()}";
    }
}