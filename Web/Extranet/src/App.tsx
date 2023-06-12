import 'bootstrap/dist/css/bootstrap.min.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Layout from './Layout';
<<<<<<< HEAD
import { Check } from './pages/Check';
import MainCalendar from './pages/MainCalendar';
import { useState } from 'react';
import { SettingDateForUser } from './models/SettingDateForUser';
=======
import MainCalendar from './pages/MainCalendarPage';
>>>>>>> 125585c430fe776b09da1b081f306bfe240cf94f



function App() {
<<<<<<< HEAD
  const [settingDateForUser, setSettingDateForUser] = useState<SettingDateForUser>(new SettingDateForUser())

=======
>>>>>>> 125585c430fe776b09da1b081f306bfe240cf94f
  return (
    <BrowserRouter> 
      <Routes>
        <Route path="/" element={<Layout />} >
<<<<<<< HEAD
        <Route index element={<MainCalendar mode = "months" settingDate={settingDateForUser}/>} />
        <Route path="/" element={<MainCalendar mode = "months" settingDate={settingDateForUser}/>} />
        <Route path="/months" element={<MainCalendar mode = "months" settingDate={settingDateForUser}/>} />
        <Route path="/weeks" element={<MainCalendar mode = "weeks" settingDate={settingDateForUser}/>} />
        <Route path="/days" element={<MainCalendar mode = "days" settingDate={settingDateForUser}/>} />
        <Route path="/check" element={<Check userId={4} mounth={5} year={2023}/>} />
=======
        <Route index element={<MainCalendar mode = "months"/>} />
        <Route path="/months" element={<MainCalendar mode = "months" />} />
        <Route path="/weeks" element={<MainCalendar mode = "weeks" />} />
        <Route path="/days" element={<MainCalendar mode = "days" />} />
>>>>>>> 125585c430fe776b09da1b081f306bfe240cf94f

        </Route>
      </Routes>
     </BrowserRouter>
  );
}

export default App;

