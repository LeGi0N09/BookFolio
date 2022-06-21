import React, { useMemo, useState } from "react";
import { Link } from "react-router-dom";
import { headerStyle } from "./style";
import List from "@material-ui/core/List";
import AppBar from "@material-ui/core/AppBar";
import ListItem from "@material-ui/core/ListItem";
import siteLogo from "../../assets/images/site-logo.svg";
import cartIcon from "../../assets/images/cart.png";
import searchIcon from "../../assets/images/search.png";
import { materialCommonStyles } from "../../utils/materialCommonStyles";
import {
	TextField,
	Button,
} from "@material-ui/core";
import Shared from "../../utils/shared";
import { AuthContextModel, useAuthContext } from "../../context/auth";
import { RoutePaths } from "../../utils/enum";
import { toast } from "react-toastify";
import { useHistory } from "react-router-dom";
import bookService from "../../service/book.service";
import { BookModel } from "../../models/BookModel";
import { CartContextModel, useCartContext } from "../../context/cart";

const Header: React.FC = () => {
	const classes = headerStyle();
	const authContext: AuthContextModel = useAuthContext();
	const cartContext: CartContextModel = useCartContext();
	const [open, setOpen] = useState<boolean>(false);
	const [query, setquery] = useState<string>("");
	const [bookList, setbookList] = useState<BookModel[]>([]);
	const [openSearchResult, setOpenSearchResult] = useState<boolean>(false);

	const history = useHistory();

	// for mobile menu
	const openMenu = (): void => {
		document.body.classList.toggle("open-menu");
	};

	const items = useMemo(() => {
		return Shared.NavigationItems.filter(
			(item) =>
				!item.access.length || item.access.includes(authContext.user.roleId)
		);
	}, [authContext.user]);

	const logOut = () => {
		authContext.signOut();
		cartContext.emptyCart();
	};

	const getBooks = async () => {
		const res = await bookService.getAll({
			pageIndex: 1,
			pageSize: 10,
			keyword: query,
		});
		setbookList(res.records);
	};

	const search = () => {
		document.body.classList.add("search-results-open");
		getBooks();
		setOpenSearchResult(true);
	};

	const addToCart = (book: BookModel): void => {
		if (!authContext.user.id) {
			toast.error("Please login before adding books to cart");
			history.push(RoutePaths.Register);
			return;
		} else {
			Shared.addToCart(book, authContext.user.id).then(res=>{
				if(res.error){
					toast.error(res.message)
				}else{
					toast.success(res.message)
					cartContext.updateCart()
				}
			})
		}
	}

	return (
		<div className={classes.headerWrapper}>
			<AppBar className="site-header" id="header" position="static">
				<div
					className="top-header"
					style={{ display: open ? "none" : "block" }}
				></div>
				<div className="bottom-header">
					<div className="container">
						<div className="header-wrapper">
							<div className="logo-wrapper">
								<Link to="/" className="site-logo" title="logo">
									<img src={siteLogo} alt="logo" />
								</Link>
							</div>
							<div className="nav-wrapper">
								<div className="top-right-bar">
									<List className="top-nav-bar">
										{!authContext.user.id && (
											<>
												<ListItem>
													<Link to={RoutePaths.Login} title="Login">
														Login
													</Link>
												</ListItem>
												<ListItem>
													<Link to={RoutePaths.Register} title="Register">
														Register
													</Link>
												</ListItem>
											</>
										)}
										
										{items.map((item, index: number) => (		
											<ListItem key={index}>
												<Link to={item.route} title={item.name}>
													{item.name}
												</Link>
											</ListItem>
										))}
									</List>
									<List className="cart-country-wrap">
										<ListItem className="cart-link">
											<Link to="/cart" title="Cart">
												<img src={cartIcon} alt="cart.png" />
												<span>{cartContext.cartData.totalRecords}</span>
												Cart
											</Link>
										</ListItem>
										<ListItem className="hamburger" onClick={openMenu}>
											<span></span>
										</ListItem>
									</List>

									{authContext.user.id && (
										<List className="right">
											<Button onClick={() => logOut()} variant="outlined">
												Log out
											</Button>
										</List>
									)}
								</div>
							</div>
						</div>
					</div>
				</div>
				<div
					className="search-overlay"
					onClick={() => {
						setOpenSearchResult(false);
						document.body.classList.remove("search-results-open");
					}}
				></div>
				<div className="header-search-wrapper">
					<div className="container">
						<div className="header-search-outer">
							<div className="header-search-inner">
								<div className="text-wrapper">
									<TextField
										id="text"
										name="text"
										placeholder="What are you looking for..."
										variant="outlined"
										value={query}
										onChange={(e: { target: { value: string } }) =>
											setquery(e.target.value)
										}
									/>

									{openSearchResult && (
										<>
											<div className="product-listing">
												{bookList?.length == 0 && (
													<p className="no-product">No product found</p>
												)}

												<p className="loading">Loading....</p>
												<List className="related-product-list">
													{bookList?.length > 0 &&
														bookList.map((item: BookModel) => {
															return (
																<ListItem>
																	<div className="inner-block">
																		<div className="left-col">
																			<span className="title">{item.name}</span>
																			<p>{item.description}</p>
																		</div>
																		<div className="right-col">
																			<span className="price">
																				{item.price}
																			</span>
																			<Link
																				to="/"
																				onClick={() => addToCart(item)}
																			>
																				Add to cart
																			</Link>
																		</div>
																	</div>
																</ListItem>
															);
														})}
												</List>
											</div>
										</>
									)}
								</div>
								<Button
									type="submit"
									className="green-btn btn"
									variant="contained"
									color="primary"
									disableElevation
									onClick={search}
								>
									<em>
										<img src={searchIcon} alt="search" />
									</em>
									Search
								</Button>
							</div>
						</div>
					</div>
				</div>
			</AppBar>
		</div>
	);
};

export default Header;