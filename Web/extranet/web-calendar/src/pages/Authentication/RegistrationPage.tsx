import { FunctionComponent, useState } from "react";
import { IValidationResult } from "../../models/IValidationResult";
import { UserService } from "../../services/UserService";
import { useNavigate } from "react-router-dom";
import { Button, Card, Col, Container, Form, Row } from "react-bootstrap";

export const RegistrationPage: FunctionComponent = () => {
    const navigate = useNavigate();

    const [login, setLogin] = useState<string>("")
    const [password, setPassword] = useState<string>("")
    const [confirmPassword, setConfirmPassword] = useState<string>("")
    const [validationResult, setValidationResult] = useState<IValidationResult>({
        error: "",
        isFail: false
    })

    const registration = async () => {
        const service: UserService = new UserService();

        let result: IValidationResult = await service.registrate({
            Login: login,
            PasswordHash: password
        });
        setValidationResult(result)

        if (password != confirmPassword) {
            setValidationResult({ isFail: true, error: "Password mismatch" })
        }

        if (!result.isFail) {
            navigate("/Authentication");
        }
    }

    return <div>
        <Container>
            <Row className="vh-100 d-flex justify-content-center align-items-center">
                <Col md={8} lg={6} xs={12}>
                    <div className="border border-2 border-primary"></div>
                    <Card className="shadow px-4">
                        <Card.Body>
                            <div className="mb-3 mt-md-4">
                                <h2 className="fw-bold mb-2 text-center text-uppercase ">Регистрация</h2>
                                <div className="mb-3">
                                    {validationResult.isFail &&
                                        <div className="alert alert-danger" role="alert">
                                            {validationResult.error}
                                        </div>
                                    }
                                    <Form>
                                        <Form.Group className="mb-3" controlId="Login">
                                            <Form.Label className="text-center">
                                                Логин
                                            </Form.Label>
                                            <Form.Control type="text" placeholder="Введи логин" onChange={l => setLogin(l.target.value)} />
                                        </Form.Group>

                                        <Form.Group
                                            className="mb-3"
                                            controlId="formBasicPassword"
                                        >
                                            <Form.Label>Пароль</Form.Label>
                                            <Form.Control type="password" placeholder="Пароль" onChange={l => setPassword(l.target.value)} />
                                        </Form.Group>
                                        <Form.Group
                                            className="mb-3"
                                            controlId="formBasicPasswordConfirm"
                                        >
                                            <Form.Label>Повтори пароль</Form.Label>
                                            <Form.Control type="password" placeholder="Пароль" onChange={l => setConfirmPassword(l.target.value)} />
                                        </Form.Group>
                                        <Form.Group
                                            className="mb-3"
                                            controlId="formBasicCheckbox"
                                        >
                                        </Form.Group>
                                        <div className="d-grid">
                                            <Button variant="primary" onClick={() => registration()}>
                                                Создать аккаунт
                                            </Button>
                                        </div>
                                    </Form>
                                    <div className="mt-3">
                                        <p className="mb-0  text-center">
                                            Уже есть аккаунт?{" "}
                                            <a href="/Authentication" className="text-primary fw-bold">
                                                Войди
                                            </a>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
        </Container>
    </div>
}

{/* <h1>Регистрация</h1>
<h2>Введите логин и пароль</h2>

<p><input type="text" name="login" value={login} onChange={l => setLogin(l.target.value)} /></p>
<p><input type="password" name="password" value={password} onChange={l => setPassword(l.target.value)} /></p>

<input type="submit" value="Регистрация" onClick={() => registration()} />
<input type="submit" value="Аунтефикация" onClick={() => navigate("/authentication")} />

<h2>{validationResult.error}</h2> */}