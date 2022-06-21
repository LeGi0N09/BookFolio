export class LoginModel {
  email!: string;
  password!: string;

  constructor() {
    this.email = "";
    this.password="";
  }
}

export class CreateUserModel {
  id?: number;
  firstName!: string;
  lastName!: string;
  email!: string;
  roleId?: number;
  password!: string;
  confirmPassword?: string;

  constructor() {
    this.id = 0;
    this.firstName = "";
    this.lastName = "";
    this.email = "";
    this.roleId = 0;
    this.password = "";
    this.confirmPassword = "";
  }
}
