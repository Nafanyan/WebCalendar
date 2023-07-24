import { FunctionComponent, useEffect } from 'react';

const GoTheAuthenticationPage: FunctionComponent = () => {
    useEffect ( () => {
        window.location.href = '/authentication';
    }, []);
    return (
        <div></div>
    );
}

export default GoTheAuthenticationPage;