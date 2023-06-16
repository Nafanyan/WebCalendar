import 'bootstrap/dist/css/bootstrap.min.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Layout from './Layout';
import { Check } from './pages/Check';
import MainCalendar from './pages/MainCalendar';
import { useState } from 'react';
import { SettingDateForUser } from './models/SettingDateForUser';
import { DateContext } from './models/ContextDateUser';



function App() {
  const [settingDateForUser, setSettingDateForUser] = useState<SettingDateForUser>(new SettingDateForUser());
  
  return (
    <BrowserRouter> 
      <Routes>
        <Route path="/" element={<Layout />} >
        {/* <Route index element={<MainCalendar mode = "months" settingDate={settingDateForUser}/>} />
        <Route path="/" element={<MainCalendar mode = "months" settingDate={settingDateForUser}/>} />
        <Route path="/months" element={<MainCalendar mode = "months" settingDate={settingDateForUser}/>} />
        <Route path="/weeks" element={<MainCalendar mode = "weeks" settingDate={settingDateForUser}/>} />
        <Route path="/days" element={<MainCalendar mode = "days" settingDate={settingDateForUser}/>} /> */}
        <Route path="/check" element={<Check />} />

        </Route>
      </Routes>
     </BrowserRouter>
  );
}

export default App;

