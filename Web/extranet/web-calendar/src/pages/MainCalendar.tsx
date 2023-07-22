import { FunctionComponent } from 'react';
import { Card, Nav } from 'react-bootstrap';
import '../css/main-calendar.css';
import { useTypedSelector } from '../hooks/UseTypeSelector';
import { SwitchMode, SwitchModeContain } from './Calendar/SwitchMode';

export interface MainCalendarProps {
    mode: string
};

export const MainCalendar: FunctionComponent<MainCalendarProps> = ({ mode }) => {
    const currDay = useTypedSelector(state => state.currentDay);

    return (
        <div>
            <Card className='nav-card-main'>
                <Card.Header className='nav-card-header'>
                    <Nav variant="tabs" defaultActiveKey={"/" + mode}>
                        <Nav.Item className='nav-link-months-year'>
                            <Nav.Link href="/Months" >
                                Месяц
                            </Nav.Link>
                        </Nav.Item>

                        <Nav.Item className='nav-link-week'>
                            <Nav.Link href="/Weeks" >
                                Неделя
                            </Nav.Link>
                        </Nav.Item>

                        <Nav.Item className='nav-link-day'>
                            <Nav.Link href="/Days" >
                                День
                            </Nav.Link>
                        </Nav.Item>

                        <Nav.Item className='nav-group-button-date'>
                            <SwitchMode mode={mode} />
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