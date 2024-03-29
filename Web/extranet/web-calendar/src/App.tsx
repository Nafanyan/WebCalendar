import 'bootstrap/dist/css/bootstrap.min.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Layout from './Layout';
import MainCalendar from './pages/MainCalendar';
import { AuthenticationPage } from './pages/Authentication/AuthenticationPage';
import { useEffect, useState } from 'react';
import { RegistrationPage } from './pages/Authentication/RegistrationPage';
import RedirectToAuthenticationPage from './pages/Authentication/RedirectToAuthenticationPage';
import { Cookies } from 'react-cookie';

function App() {
  const [userAuth, setUserAuth] = useState<boolean>(Boolean(localStorage.getItem("token-is-valid")));

  useEffect(() => {
    let tokenIsValid: boolean = Boolean(localStorage.getItem("token-is-valid"));
    const cookies = new Cookies();

    if (!tokenIsValid && cookies.get("RefreshToken") == null) {
      localStorage.removeItem("token-is-valid");
      localStorage.removeItem("user-name");
      setUserAuth(tokenIsValid);
    }
  }, [])

  return (
    <BrowserRouter>
      <Routes>
        {userAuth ?
          <>
            <Route path="/" element={<Layout />} >
              <Route index element={<MainCalendar mode="months" />} />
              <Route path="/" element={<MainCalendar mode="months" />} />
              <Route path="/months" element={<MainCalendar mode="months" />} />
              <Route path="/weeks" element={<MainCalendar mode="weeks" />} />
              <Route path="/days" element={<MainCalendar mode="days" />} />
            </Route>
            <Route path="*" element={<RedirectToAuthenticationPage />} />
          </>
          :
          <>
            <Route path="/registration" element={<RegistrationPage />} />
            <Route path="/authentication" element={<AuthenticationPage />} />
            <Route path="*" element={<RedirectToAuthenticationPage />} />
          </>
        }
      </Routes>
    </BrowserRouter>
  );
}

export default App;