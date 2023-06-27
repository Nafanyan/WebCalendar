import { TypedUseSelectorHook, useSelector } from "react-redux";
import { RootState } from "../store/reduces";

export const useTypedSelector: TypedUseSelectorHook<RootState> = useSelector