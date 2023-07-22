import 'bootstrap/dist/css/bootstrap.min.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Layout from './Layout';
import MainCalendar from './pages/MainCalendar';
import { AuthenticationPage } from './pages/Authentication/AuthenticationPage';
import { useEffect, useState } from 'react';
import { RegistrationPage } from './pages/Authentication/RegistrationPage';
import { TokenDecoder } from './custom-utils/TokenDecoder';
import NotAuthenticationPage from './pages/Authentication/NotAuthenticationPage';
import { AuthenticationService } from './services/AuthenticationService';
import NotFoundPage from './pages/NotFoundPage';

function App() {
  const [userAuth, setUserAuth] = useState<boolean>(Boolean(localStorage.getItem("token-is-valid")));

  useEffect(() => {
    setUserAuth(Boolean(localStorage.getItem("token-is-valid")));
  }, [])

  return (
    <BrowserRouter>
      <Routes>

        {userAuth ?
          <>
            <Route path="/" element={<Layout />} >
              <Route index element={<MainCalendar mode="Months" />} />
              <Route path="/" element={<MainCalendar mode="Months" />} />
              <Route path="/Months" element={<MainCalendar mode="Months" />} />
              <Route path="/Weeks" element={<MainCalendar mode="Weeks" />} />
              <Route path="/Days" element={<MainCalendar mode="Days" />} />
            </Route>
            <Route path="*" element={<NotFoundPage />} />
          </>
          :
          <>
            <Route path="/Registration" element={<RegistrationPage />} />
            <Route path="/Authentication" element={<AuthenticationPage />} />
            <Route path="*" element={<NotAuthenticationPage />} />
          </>
        }
      </Routes>
    </BrowserRouter>
  );
}

export default App;