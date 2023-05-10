import { FunctionComponent} from 'react';
import { Button, Card, Table } from 'react-bootstrap';
import "../../css/MonthCalendar.css"

export interface MonthCalendarProps {
    days: number
}

export const MonthCalendar: FunctionComponent<MonthCalendarProps> = ({ days }) => {
    let daysWeek: string[] = ['ПН', 'ВТ', 'СР', 'ЧТ', 'ПТ', 'СБ', 'ВС']
    let daysMounth = [[1, 2, 3, 4, 5, 6, 7], [8, 9, 10, 11, 12, 13, 14], [15, 16, 17, 18, 19, 20, 21], [22, 23, 24, 25, 26, 27, 28], [29, 30, 31]]

    return (<div>
        <Table striped bordered hover>
            <thead>
                <tr>
                    {daysWeek.map((day) =>
                    (
                        <th className='day-of-week'>
                            {day}
                        </th>
                    )
                    )}
                </tr>
            </thead>

            <tbody>
                {daysMounth.map(
                    (week, key) => (
                        <tr key={key}>
                            {week.map(
                                (day) => (
                                    <th>
                                        <Card className='day-of-months'>
                                            <Card.Header className='card-day-header'>
                                                {day}
                                                <button className='add-event-button'>+</button>
                                            </Card.Header>
                                            <Card.Body className='card-day-text'>
                                                <Card.Text >
                                                    <Button variant="light">8:00-16:00</Button>{' '}
                                                    Работа
                                                </Card.Text>
                                                <Card.Text  >
                                                    <Button variant="light">17:00-20:00</Button>{' '}
                                                    Репетиция
                                                </Card.Text>
                                            </Card.Body>
                                        </Card>
                                    </th>
                                )
                            )
                            }
                        </tr>
                    )
                )}
                {/* <tr>
                    {Array.from({ length: 7 }).map((_, index) => (
                        <th key={index}>
                            <Card border="primary" style={{ width: '9rem' }} className='day-of-months'>
                                <Card.Header>{index + 1}</Card.Header>
                                <Card.Body>
                                    <Card.Text>
                                        Time Events
                                    </Card.Text>
                                </Card.Body>
                            </Card>
                            <br />
                        </th>
                    ))}
                </tr> */}
                {/* <tr>
                    {Array.from({ length: 7 }).map((_, index) => (
                        <th key={index}>
                            <Card border="primary" style={{ width: '9rem' }}>
                                <Card.Header>{index + 8}</Card.Header>
                                <Card.Body>
                                    <Card.Text>
                                        Time Events
                                    </Card.Text>
                                </Card.Body>
                            </Card>
                            <br />
                        </th>
                    ))}
                </tr>
                <tr>
                    {Array.from({ length: 7 }).map((_, index) => (
                        <th key={index}>
                            <Card border="primary" style={{ width: '9rem' }}>
                                <Card.Header>{index + 15}</Card.Header>
                                <Card.Body>
                                    <Card.Text>
                                        Time Events
                                    </Card.Text>
                                </Card.Body>
                            </Card>
                            <br />
                        </th>
                    ))}
                </tr>
                <tr>
                    {Array.from({ length: 7 }).map((_, index) => (
                        <th key={index}>
                            <Card border="primary" style={{ width: '9rem' }}>
                                <Card.Header>{index + 22}</Card.Header>
                                <Card.Body>
                                    <Card.Text>
                                        Time Events
                                    </Card.Text>
                                </Card.Body>
                            </Card>
                            <br />
                        </th>
                    ))}
                </tr>
                <tr>
                    {Array.from({ length: 3 }).map((_, index) => (
                        <th key={index}>
                            <Card border="primary" style={{ width: '9rem' }}>
                                <Card.Header>{index + 29}</Card.Header>
                                <Card.Body>
                                    <Card.Text>
                                        Time Events
                                    </Card.Text>
                                </Card.Body>
                            </Card>
                            <br />
                        </th>
                    ))}
                </tr> */}
            </tbody>
        </Table>
    </div>)
}

export default MonthCalendar;