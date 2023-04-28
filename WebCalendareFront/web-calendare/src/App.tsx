import './css/App.css';

// import 'bootstrap/dist/css/bootstrap.min.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Layout from './Layout';
import MainCalendare from './pages/MainCalendare';

function App() {
  return (
    <BrowserRouter> 
      <Routes>
        <Route path="/" element={<Layout />} >
        <Route index element={<MainCalendare days={31}/>} />
        <Route path="days"> <h1> days</h1></Route>
        {/* <Route path="days"> <h1> days</h1></Route> */}

        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;

