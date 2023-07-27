import { FunctionComponent, useEffect, useState } from "react";
import { Outlet } from "react-router-dom";
import './css/layout.css';
import { Cookies } from "react-cookie";
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';

const Layout: FunctionComponent = () => {
    const [userLogin, setUserLogin] = useState<string>("");

    useEffect(() => {
        setUserLogin((localStorage.getItem('user-name') + "").replaceAll("\"", ""))
    }, [])

    const exit = () => {
        localStorage.removeItem("access-token");
        localStorage.removeItem("token-is-valid");
        localStorage.removeItem("user-name");
        
        const cookies = new Cookies();
        cookies.remove("RefreshToken");
        window.location.href = '/authentication';
    }

    return (
        <>
            <header>
                <Navbar expand="lg">
                    <Container fluid>
                        <Navbar.Brand href="/" className="home-link">Web-Calendar</Navbar.Brand>
                        <Navbar.Collapse id="navbar-dark-example" className="options">
                            <Nav>
                                <NavDropdown
                                    id="nav-dropdown-dark-example"
                                    title={userLogin}
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
            </footer>
        </>
    )
};

export default Layout;