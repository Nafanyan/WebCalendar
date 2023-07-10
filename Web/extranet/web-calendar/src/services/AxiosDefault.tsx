import axios from "axios";

export default axios.create({
    // baseURL: "http://localhost:24395/" for debug
    headers: {
        "Accept": "application/json",
        "Content-Type": "application/json"
    },
});