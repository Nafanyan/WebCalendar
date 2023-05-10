
import { FunctionComponent, useState } from 'react';
import { useLocation } from 'react-router-dom';
import MonthCalendar from './MonthCalendar';
import WeekCalendare from './WeekCalendar';
import DayCalendare from './DayCalendar';
import { ButtonGroup, Dropdown, DropdownButton, Nav } from 'react-bootstrap';
import '../../css/MainCalendar.css'
import { Console } from 'console';


export const SwitchModeContain: FunctionComponent = () => {
    const location = useLocation()
    if (location.pathname === "/weeks") {
        return <WeekCalendare />;
    }
    else if (location.pathname === "/days") {
        return <DayCalendare />;
    }
    else {
        return <MonthCalendar days={31} />;
    }
}


export const SwitchModeDateSelection: FunctionComponent = () => {
    const location = useLocation();
    let months = ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь', 'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'];
    const [stateMonth, setStateMonth] = useState<string>(months[4]);

    if (location.pathname === "/weeks") {
        return (
            <Nav.Item>
                < DropdownButton id="dropdown-week-button" title="01.05 - 07.05">
                    <Dropdown.Item href="#/action-1">Action</Dropdown.Item>
                    <Dropdown.Item href="#/action-2">Another action</Dropdown.Item>
                    <Dropdown.Item href="#/action-3">Something else</Dropdown.Item>
                </DropdownButton >
            </Nav.Item>
        );
    }
    if (location.pathname === "/days") {

        return (
            <Nav.Item>
                < DropdownButton id="dropdown-day-button" title="01.05.2023" >
                    <Dropdown.Item href="#/action-1">Action</Dropdown.Item>
                    <Dropdown.Item href="#/action-2">Another action</Dropdown.Item>
                    <Dropdown.Item href="#/action-3">Something else</Dropdown.Item>
                </DropdownButton >
            </Nav.Item>
        );
    }

    return (
        <Nav.Item>
            <DropdownButton
                as={ButtonGroup}
                key={'year'}
                id={'dropdown-year-button'}
                title={'2023'}>

                <Dropdown.Item eventKey="1">Action</Dropdown.Item>
                <Dropdown.Item eventKey="2">Another action</Dropdown.Item>
                <Dropdown.Item eventKey="3" active>
                    Active Item
                </Dropdown.Item>
                <Dropdown.Divider />
                <Dropdown.Item eventKey="4">Separated link</Dropdown.Item>
            </DropdownButton>

            <DropdownButton
                as={ButtonGroup}
                key={'month'}
                id={'dropdown-month-button'}
                title={stateMonth}>

                {months.map(
                    (month) => (
                        <Dropdown.Item eventKey={months.indexOf(month)} >{month}</Dropdown.Item>
                    )
                )}
            </DropdownButton>
        </Nav.Item>
    );
}
export default SwitchModeContain;