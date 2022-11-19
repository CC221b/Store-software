
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
    Get,//קריאה לפי ת"ז
    GetAll,//קריאת כל הישיות
    Update,//עדכון
    Delete,//מחיקה
    //פונקציות בשביל פרטי הזמנה:
    GetByProductIDandOrderID,//קריאת פריט ע"י מזהה מוצר ומזהה הזמנה
    GetByOrderID,//רשימת פריטים ע"י מזהה הזמנה
}




