import { FunctionComponent } from "react";
import { Outlet } from "react-router-dom";
import './css/layout.css';
import { Cookies } from "react-cookie";
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';

const Layout: FunctionComponent = () => {
    const exit = () => {
        localStorage.removeItem("token-is-valid")
        const cookies = new Cookies()
        cookies.remove("RefreshToken")
        window.location.href = '/Authentication';
    }

    return (
        <>
            <header>
                {/* <nav>
                    <a href="/" className="home-link">Web-Calendar</a>
                </nav> */}
                <Navbar expand="lg">
                    <Container fluid>
                        <Navbar.Brand href="/" className="home-link">Web-Calendar</Navbar.Brand>
                        <Navbar.Collapse id="navbar-dark-example" className="options">
                            <Nav>
                                <NavDropdown
                                    id="nav-dropdown-dark-example"
                                    title="Опции"
                                >
                                    <NavDropdown.Item onClick={() => exit()}>Выйти</NavDropdown.Item>
                                </NavDropdown>
                            </Nav>
                        </Navbar.Collapse>
                    </Container>
                </Navbar>
            </header>
            <Outlet />
            <footer>
                {/* <p>
                    <input type="submit" value="Выйти" onClick={} />
                </p> */}
            </footer>
        </>
    )
};

export default Layout;