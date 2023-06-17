import { FunctionComponent, useContext, useEffect, useState } from "react";
import UserList from "./UserList";
import { useTypedSelector } from "../hooks/useTypeSelector";


export const Check: FunctionComponent = () => {
    const day = useTypedSelector(state => state.currentDay);

    useEffect(() => {

        console.log(day);
    }, [day])


    return <div>
        <UserList />
    </div>
}