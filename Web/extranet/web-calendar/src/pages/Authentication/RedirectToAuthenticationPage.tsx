import { FunctionComponent, useEffect } from 'react';

const RedirectToAuthenticationPage: FunctionComponent = () => {
    useEffect ( () => {
        window.location.href = '/authentication';
    }, []);
    return (
        <div></div>
    );
}

export default RedirectToAuthenticationPage;