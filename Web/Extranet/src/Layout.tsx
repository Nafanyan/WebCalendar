import { FunctionComponent } from "react";
import { Outlet } from "react-router-dom";
import './css/layout.css';

const Layout: FunctionComponent = () => {
    return (
        <>
            <header>
                <nav>
                    <a href="/" className="home-link">Web-Calendar</a>
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