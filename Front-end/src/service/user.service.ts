import request from "./request";
import Role from "../models/RoleModel";
import FilterModel from "../models/FilterModel";
import BaseList from "../models/BaseList";
import UserModel, { AddOrEditUserModel } from "../models/UserModel";

class AuthService {
  ENDPOINT = "api/User";
  ENDPOINT1 = "api/role"
  public async getAllUsers(params: FilterModel): Promise<BaseList<UserModel[]>> {
    const url = `${this.ENDPOINT}/list`;
    return request.get<BaseList<UserModel[]>>(url, {params}).then((res) => {
      return res.data;
    });
  }

  public async getAllRoles(): Promise<BaseList<Role[]>> {
    const url = `${this.ENDPOINT1}/list`;
    return request.get<BaseList<Role[]>>(url).then((res) => {
      return res.data;
    });
  }

  public async getById(id: number): Promise<UserModel> {
    const url = `${this.ENDPOINT}/{id}`;
    return request.get<UserModel>(url).then((res) => {
      return res.data;
    });
  }

  public async delete(id: number): Promise<UserModel> {
    const url = `${this.ENDPOINT}/delete/${id}`;
    return request.delete<UserModel>(url).then((res) => {
      console.log("abhis...",res.data)
      return res.data;
    });
  }

  public async update(data: AddOrEditUserModel): Promise<AddOrEditUserModel> {
    const url = `${this.ENDPOINT}/add`;
    return request.put<AddOrEditUserModel>(url, data ).then((res) => {
      return res.data;
    });
  }

  public async updateProfile(data: UserModel): Promise<UserModel> {
    const url = `${this.ENDPOINT}/update`;
    return request.put<UserModel>(url, data ).then((res) => {
      return res.data;
    });
  }
}
export default new AuthService();
