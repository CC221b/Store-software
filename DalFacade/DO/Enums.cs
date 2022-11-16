
namespace DO;

public enum Categories
{
    Percussion,//הקשה
    Stringed,//מיתרים
    Keyboard,//קלידים
    Wind,//נשיפה
    Electronic//אלקטרוני
}

public enum Options
{
    Add,//הוספה
    Read,//קריאה לפי ת"ז
    ReadAll,//קריאת כל הישיות
    Update,//עדכון
    Delete,//מחיקה
    //פונקציות בשביל פרטי הזמנה:
    ReadByProductIDandOrderID,//קריאת פריט ע"י מזהה מוצר ומזהה הזמנה
    ReadByOrderID,//רשימת פריטים ע"י מזהה הזמנה
}




