import { IDecryptedToken } from "../models/IDecryptedToken";
import jwtDecode from "jwt-decode";

export function TokenDecoder(token: string ) {
    const decoded = jwtDecode<IDecryptedToken>(token);
    return decoded;
}