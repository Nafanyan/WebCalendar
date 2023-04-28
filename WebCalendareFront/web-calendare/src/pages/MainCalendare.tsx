import { FunctionComponent, useEffect, useState } from 'react';
import { Button, Card, Container, Row } from 'react-bootstrap';
import Table from 'react-bootstrap/Table';

export interface MainCalendareProps {
    days: number
}


export const MainCalendare: FunctionComponent<MainCalendareProps> = ({ days }) => {
    const [days2DArray, setDays2DArray] = useState<number[][]>([])

    useEffect(() => {
        const fetch = async () => {
            let days1DArray: number[] = [];
            for (let i = 1; i <= days; i++) {
                days1DArray.push(i);
            }
            let days2DArray: number[][] = [];
            for (let i = 0; i < days; i + 7) {
                if (i > days) {
                    i = days - 8;
                }
                days2DArray.push(days1DArray.slice(i, i + 7));
            }
            setDays2DArray(days2DArray);
        }
    }, [])
    console.log(days2DArray);
    return (
        <div>

            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>Monday</th>
                        <th>Tuesday</th>
                        <th>Wednsday</th>
                        <th>Thursday</th>
                        <th>Friday</th>
                        <th>Saturday</th>
                        <th>Sunday</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        {Array.from({ length: 7 }).map((_, index) => (
                            <th key={index}>
                                <Card border="primary" style={{ width: '9rem' }}>
                                    <Card.Header>{index}</Card.Header>
                                    <Card.Body>
                                        <Card.Text>
                                            Time Events
                                        </Card.Text>
                                        <Button variant="primary">Read more</Button>
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
                                    <Card.Header>{index + 7}</Card.Header>
                                    <Card.Body>
                                        <Card.Text>
                                            Time Events
                                        </Card.Text>
                                        <Button variant="primary">Read more</Button>
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
                                    <Card.Header>{index + 14}</Card.Header>
                                    <Card.Body>
                                        <Card.Text>
                                            Time Events
                                        </Card.Text>
                                        <Button variant="primary">Read more</Button>
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
                                    <Card.Header>{index + 21}</Card.Header>
                                    <Card.Body>
                                        <Card.Text>
                                            Time Events
                                        </Card.Text>
                                        <Button variant="primary">Read more</Button>
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
                                    <Card.Header>{index + 28}</Card.Header>
                                    <Card.Body>
                                        <Card.Text>
                                            Time Events
                                        </Card.Text>
                                        <Button variant="primary">Read more</Button>
                                    </Card.Body>
                                </Card>
                                <br />
                            </th>
                        ))}
                    </tr>
                </tbody>
            </Table>
        </div>
    );
}

export default MainCalendare;