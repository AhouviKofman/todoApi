import axios from 'axios';

const apiUrl = "http://localhost:5279"
//מגדיר את כתובת ה- API המוגדרת כברירת מחדל 
axios.defaults.baseURL = "http://localhost:5019";

//השינוי השני מוסיף אינטרספטור שתופס את השגיאות בתגובה ומכתיב אותם ללוג. בעזרת הפונקציה 
//
axios.interceptors.response.use(
  (response) => response,
  (error) => {
    console.error('Error:', error);
    return Promise.reject(error);
  }
);

export default {


  //שליפה
  getTasks: async () => {
    const result = await axios.get(`${apiUrl}/`)    
    return result.data;
  },
 //הוספה
  addTask: async (name) => {
    debugger
    // const result = await axios.post(`${apiUrl}/${name}`) 
    const result = await axios.post(`${apiUrl}/${name}`) 
    // const newItem = response.data;
    console.log('addTask',name)
    return {};
  },
  
  
   //עדכון
   setCompleted: async(id, isComplete)=>{
    const result = await axios.put(`${apiUrl}/${id}/${isComplete}`) 
    console.log('setCompleted', {id, isComplete})
    //TODO
    return {};
  },
//מחיקה
  deleteTask:async(id)=>{
   
    const result = await axios.delete(`${apiUrl}/${id}`) 
    
    return {};
  }
}

;

