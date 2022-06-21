import { BookModel } from "../models/BookModel";
import UserModel from "../models/UserModel";
import cartService from "../service/cart.service";
import { Role, RoutePaths } from "./enum";

export default class Shared {
	public static messages = {
		USER_DELETE: "Are you sure you want to delete this user?",
		UPDATED_SUCCESS: "Record updated successfully",
		DELETE_SUCCESS: "Record deleted successfully",
		ORDER_SUCCESS: "Your order is successfully placed",
	};

	public static LocalStorageKeys = {
		USER: "user",
	};

	public static NavigationItems = [
		{
			name: "Users",
			route: RoutePaths.User,
			access: [Role.Admin],
		},
		{
			name: "Categories",
			route: RoutePaths.Category,
			access: [Role.Admin],
		},
		{
			name: "Books",
			route: RoutePaths.Book,
			access: [Role.Admin, Role.Seller],
		},
		{
			name: "Update Profile",
			route: RoutePaths.UpdateProfile,
			access: [Role.Admin, Role.Buyer, Role.Seller],
		},
	];
        
	public static hasAccess = (pathname: string, user: UserModel): boolean => {
		
		const navItem = Shared.NavigationItems.find((navItem) =>
		{//console.log("abhi",navItem)
			pathname.includes(navItem.route)}
		);
		
		if (navItem) {
			return (
				!navItem.access ||
				!!(navItem.access && navItem.access.includes(user.roleId))
			);
		}
		return true;
	};
	public static addToCart = (
		book: BookModel,
		id: number
	): Promise<{ error: boolean; message: string }> => {
		return cartService
      .add({
        userId: id,
        bookId: book.id as number,
        quantity: 1,
      })
      .then((res) => {
        return { error: false, message: "Item added in cart" };
      })
      .catch((e) => {
        if (e.status === 500)
          return { error: true, message: "Item already in the cart" };
        else
          return { error: true, message: "something went wrong" };
      });
	};
}
