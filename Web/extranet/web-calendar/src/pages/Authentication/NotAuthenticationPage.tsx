import { FunctionComponent, useEffect } from 'react';

const NotAuthenticationPage: FunctionComponent = () => {
    useEffect ( () => {
        window.location.href = '/Authentication'
    }, [])
    return (
        <div></div>
    );
}

export default NotAuthenticationPage;