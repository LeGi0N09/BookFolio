import { CreateUserModel, LoginModel } from "../models/AuthModel";
import UserModel from "../models/UserModel";
import request from "./request";

class AuthService {
    ENDPOINT = 'api/public';

    public async login(data: LoginModel): Promise<UserModel> {
        const url = `${this.ENDPOINT}/login`;
        return request.post(url, data).then((res) => {
            return res.data as UserModel;
        });
    }

    public async create(data: CreateUserModel): Promise<CreateUserModel> {
        const url = `${this.ENDPOINT}/Register`;
        return request.post<CreateUserModel>(url, data ).then((res) => {
            return res.data;
        });
    }
}
export default new AuthService();
