import { createContext, useContext, useEffect, useState } from "react";
import React from "react";
import Shared from "../utils/shared";
import { RoutePaths } from "../utils/enum";
import UserModel from "../models/UserModel";
import { useHistory, useLocation } from "react-router-dom";
import { toast } from "react-toastify";
import { CartList, GetCart } from "../models/CartModel";
import cartService from "../service/cart.service";
import { AuthContextModel, useAuthContext } from "./auth";

export interface CartContextModel {
	cartData: GetCart;
	updateCart: () => void;
	emptyCart:() =>void;
}

const initialState: CartContextModel = {
	cartData: {
		records: [],
		totalRecords: 0,
	},
	updateCart: () => {},
	emptyCart: () => {}
};

export const CartContext = createContext(initialState);

export const CartWrapper: React.FC<React.PropsWithChildren<{}>> = ({
	children,
}: React.PropsWithChildren<{}>) => {
	const authContext: AuthContextModel = useAuthContext();

	const [cartData, setCartData] = useState<GetCart>({
		totalRecords: 0,
		records: [],
	});
	useEffect(() => {
		updateCart();
	}, [authContext.user.id]);

	const updateCart = () => {
		if (authContext.user.id) {
			cartService.getList(authContext.user.id).then((res) => setCartData(res));
		}
	};
	const emptyCart=()=>{
		setCartData({
			totalRecords:0,
			records:[]
		});
	}
	let value = {
		cartData,
		updateCart,
		emptyCart,
	};
	return <CartContext.Provider value={value}>{children}</CartContext.Provider>;
};

export const useCartContext = () => {
	return useContext(CartContext);
};
