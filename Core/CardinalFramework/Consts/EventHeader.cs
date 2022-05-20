namespace Cardinal.Consts;

public static class EventHeader
{
    public enum Code
    {
        IDENTIFIER,
        TARGET,
        PAYLOAD
    }

    public static string toS(Code code)
    {
        return Enum.GetName(typeof(Code), code) ?? "";
    }

    public static string toC(Code code)
    {
        switch(code) {
            case Code.IDENTIFIER:
                return "0x01";
            case Code.TARGET:
                return "0x02";
            case Code.PAYLOAD:
                return "0x03";
            default:
                return "";
        }
    }
}