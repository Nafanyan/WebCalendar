import 'bootstrap/dist/css/bootstrap.min.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Layout from './Layout';
import MainCalendar from './pages/MainCalendar';
import { AuthenticationPage } from './pages/AuthenticationPage';

function App() {

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout />} >
          <Route index element={<MainCalendar mode="months" />} />
          <Route path="/" element={<MainCalendar mode="months" />} />
          <Route path="/months" element={<MainCalendar mode="months" />} />
          <Route path="/weeks" element={<MainCalendar mode="weeks" />} />
          <Route path="/days" element={<MainCalendar mode="days" />} />
          <Route path="/authetication" element={<AuthenticationPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;

