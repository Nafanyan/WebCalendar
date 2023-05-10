import { FunctionComponent } from 'react';
import { Card, Nav, } from 'react-bootstrap';
import SwitchModeContain, { SwitchModeDateSelection } from './Calendar/SwitchMode';
import '../css/MainCalendar.css'


export interface MainCalendarProps {
    mode: string
}

export const MainCalendar: FunctionComponent<MainCalendarProps> = ({ mode }) => {
    if (mode === "/") {
        mode = 'months'
    }

    return (
        <div>
            <Card className='nav-card-main'>
                <Card.Header className='nav-card-header'>
                    <Nav variant="tabs" defaultActiveKey={"/" + mode}>
                        <Nav.Item className='nav-link-months-year'>
                            <Nav.Link href="/months" >
                                Месяц
                            </Nav.Link>
                        </Nav.Item>

                        <Nav.Item className='nav-link-week'>
                            <Nav.Link href="/weeks" >
                                Неделя
                            </Nav.Link>
                        </Nav.Item>

                        <Nav.Item className='nav-link-day'>
                            <Nav.Link href="/days" >
                                День
                            </Nav.Link>
                        </Nav.Item>
                        
                        <Nav.Item className='nav-group-button-date'>
                            <SwitchModeDateSelection />
                        </Nav.Item>
                    </Nav>
                </Card.Header>
                
                <Card.Body className='content-card-body'>
                    <SwitchModeContain />
                </Card.Body>
            </Card>
        </div>
    );
}

export default MainCalendar;