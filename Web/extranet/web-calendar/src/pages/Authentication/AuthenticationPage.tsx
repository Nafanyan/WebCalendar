import { FunctionComponent, useState } from "react";
import { useDispatch } from "react-redux";
import { IValidationResult } from "../../models/IValidationResult";
import { ITokenCommandResult } from "../../models/result-request/Tokens/ITokenCommandResult";
import { AuthenticationService } from "../../services/AuthenticationService";
import { useNavigate } from "react-router-dom";
import { CurrentDayActionType } from "../../models/type/currentDay";
import { TokenDecoder } from "../../custom-utils/TokenDecoder";
import { Button, Card, Col, Container, Form, Row } from "react-bootstrap";

export const AuthenticationPage: FunctionComponent = () => {
    const dispatch = useDispatch()
    const navigate = useNavigate();

    const [login, setLogin] = useState<string>("")
    const [password, setPassword] = useState<string>("")
    const [validationResult, setValidationResult] = useState<IValidationResult>({
        error: "",
        isFail: false
    })

    const authenticationWithLogin = async () => {
        const service: AuthenticationService = new AuthenticationService();

        let result: ITokenCommandResult = await service.authentication({
            Login: login,
            PasswordHash: password
        });
        setValidationResult(result.validationResult)

        if (!result.validationResult.isFail) {
            dispatch({
                type: CurrentDayActionType.CHANGE_USER_ID,
                userId: TokenDecoder(localStorage.getItem('access-token') + "").userId
            })

            localStorage.removeItem("token-is-valid")
            localStorage.setItem("token-is-valid", JSON.stringify(true))
            window.location.href = '/Months'
        }
    }

    return (
        <Container>
            <Row className="vh-100 d-flex justify-content-center align-items-center">
                <Col md={8} lg={6} xs={12}>
                    <Card className="px-4">
                        <Card.Body>
                            <div className="mb-3 mt-md-4">
                                <h2 className="fw-bold mb-2 text-center text-uppercase ">
                                    Вход
                                </h2>
                                <div className="mb-3">
                                    {validationResult.isFail &&
                                        <div className="alert alert-danger" role="alert">
                                            {validationResult.error}
                                        </div>
                                    }
                                    <Form>
                                        <Form.Group className="mb-3" controlId="Login">
                                            <Form.Label className="text-center">Логин</Form.Label>
                                            <Form.Control type="text" placeholder="Введите логин" onChange={l => setLogin(l.target.value)} />
                                        </Form.Group>
                                        <Form.Group
                                            className="mb-3"
                                            controlId="formBasicPassword"
                                        >
                                            <Form.Label>Пароль</Form.Label>
                                            <Form.Control type="password" placeholder="Введите пароль" onChange={l => setPassword(l.target.value)} />
                                        </Form.Group>
                                        <div className="d-grid">
                                            <Button variant="primary" onClick={() => authenticationWithLogin()}>
                                                Войти
                                            </Button>
                                        </div>
                                    </Form>
                                    <div className="mt-3">
                                        <p className="mb-0  text-center">
                                            Нет аккаунта?{" "}
                                            <a href="/Registration" className="text-primary fw-bold">
                                                Зарегистрируйся
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
    );
}

{/* <h1>Аунтефикация</h1>

        {authWithToken &&
            <>
                <h2>Это ваш аккаунт? {login}</h2>

                <input type="submit" value="Да, войти" onClick={() => authenticationWithToken()} />
                <input type="submit" value="Нет" onClick={() => clearCookie()} />
            </>
        }

        <h2>Введите логин и пароль</h2>

        <p><input type="text" name="login" value={login} onChange={l => setLogin(l.target.value)} /></p>
        <p><input type="password" name="password" value={password} onChange={l => setPassword(l.target.value)} /></p>

        <input type="submit" value="Аунтефикация" onClick={() => authenticationWithLogin()} />
        <input type="submit" value="Регистрация" onClick={() => navigate("/registration")} />
        <h2>{validationResult.error}</h2> */}