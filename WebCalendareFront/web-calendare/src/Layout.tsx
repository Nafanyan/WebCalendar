import { FunctionComponent } from "react";
import {  Outlet } from "react-router-dom";
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Button, Card } from "react-bootstrap";

const Layout: FunctionComponent = () => {
    return (
        <>
            <Navbar bg="light" expand="lg">
                <Container>
                    <Navbar.Brand href="#months">Web-Calendare</Navbar.Brand>
                    <Navbar.Toggle aria-controls="basic-navbar-nav" />
                    <Navbar.Collapse id="basic-navbar-nav">
                        <Nav className="me-auto">
                            <Nav.Link href="#settings">Settings</Nav.Link>
                            <Nav.Link href="#profils">Profils</Nav.Link>
                        </Nav>
                    </Navbar.Collapse>
                </Container>
            </Navbar>
            <Card>
                <Card.Header>
                    <Nav variant="tabs" defaultActiveKey="#first">
                        <Nav.Item>
                            <Nav.Link href="#months">Months Years </Nav.Link>
                        </Nav.Item>
                        <Nav.Item>
                            <Nav.Link href="#weeks">Weeks</Nav.Link>
                        </Nav.Item>
                        <Nav.Item>
                            <Nav.Link href="#days">Days</Nav.Link>
                        </Nav.Item>
                    </Nav>
                </Card.Header>
                <Card.Body>
                    <Outlet />
                </Card.Body>
            </Card>

        </>
    )
};

export default Layout;