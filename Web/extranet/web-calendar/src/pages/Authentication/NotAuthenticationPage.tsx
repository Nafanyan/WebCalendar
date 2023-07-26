import { FunctionComponent, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

const NotAuthenticationPage: FunctionComponent = () => {
    const navigate = useNavigate();
    useEffect ( () => {
        navigate("/Authentication")
    }, [])
    return (
        <div></div>
    );
}

export default NotAuthenticationPage;