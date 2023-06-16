import { FunctionComponent, useContext, useEffect, useState } from "react";
import { DateContext, IContextDateUser, defaultState } from "../models/ContextDateUser";


export const Check: FunctionComponent = () => {
    // const {dateUser} = useContext(IContextDateUser);
    const [dateUser, setDateUser] = useState<IContextDateUser>(defaultState)
    console.log(dateUser.userId);
    return <div>
        <button onClick={() => setDateUser({
            userId: 0,
            year: new Date().getFullYear(),
            month: new Date().getMonth() + 1,
            day: new Date().getDate(),
        })}></button>
    </div>
}