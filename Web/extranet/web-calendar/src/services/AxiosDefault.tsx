import axios from "axios";

export default axios.create({
    baseURL: "http://localhost:90/",
    headers: {
        "Accept": "application/json",
        "Content-Type": "application/json"
    },
});