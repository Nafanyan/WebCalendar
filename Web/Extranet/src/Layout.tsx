import { FunctionComponent } from "react";
import { Outlet } from "react-router-dom";
<<<<<<< HEAD
import './css/layout.css';
=======
import './css/Layout.css';
>>>>>>> 125585c430fe776b09da1b081f306bfe240cf94f

const Layout: FunctionComponent = () => {
    return (
        <>
            <header>
                <nav>
<<<<<<< HEAD
                    <a href="/" className="home-link">Web-Calendar</a>
=======
                    <a href="/" className="home-link">Web-Calendare</a>
>>>>>>> 125585c430fe776b09da1b081f306bfe240cf94f
                </nav>
            </header>
            <Outlet />
            <footer>
                <p>
                    Footer
                </p>
            </footer>
        </>
    )
};

export default Layout;