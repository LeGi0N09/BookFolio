import React, { lazy } from "react";
import { Route, Switch } from "react-router-dom";
import { RoutePaths } from "../utils/enum";
import PrivateRoute from "./PrivateRoute";

//component lazy loading
const Login = lazy(() => import("../pages/login/index"));
const Register = lazy(() => import("../pages/register/index"));
const User = lazy(() => import("../pages/user/index"));
const EditUser = lazy(() => import("../pages/user/editUser/index"));
const Category = lazy(() => import("../pages/category/index"));
const EditCategory = lazy(() => import("../pages/category/editCategory/index"));
const Book = lazy(() => import("../pages/book/index"));
const EditBook = lazy(() => import("../pages/book/editBook/index"));
const BookList = lazy(() => import("../pages/book-listing/index"));
const Cart = lazy(() => import("../pages/cart/index"));
const UpdateProfile = lazy(() => import("../pages/update-profile/index"));

const AppRoutes: React.FC = () => {
  return (
    <Switch>
      <Route exact path={RoutePaths.Login} component={Login} />
      <Route exact path={RoutePaths.Register} component={Register} />
      <Route exact path={RoutePaths.BookListing} component={BookList} />
      <PrivateRoute exact path={RoutePaths.User} component={User} />
      <PrivateRoute exact path={RoutePaths.EditUser} component={EditUser} />
      <PrivateRoute exact path={RoutePaths.Category} component={Category} />
      <PrivateRoute
        exact
        path={RoutePaths.EditCategory}
        component={EditCategory}
      />
      <PrivateRoute
        exact
        path={RoutePaths.AddCategory}
        component={EditCategory}
      />
      <PrivateRoute exact path={RoutePaths.Book} component={Book} />
      <PrivateRoute exact path={RoutePaths.EditBook} component={EditBook} />
      <PrivateRoute exact path={RoutePaths.AddBook} component={EditBook} />

      <PrivateRoute exact path={RoutePaths.Cart} component={Cart} />
      <PrivateRoute
        exact
        path={RoutePaths.UpdateProfile}
        component={UpdateProfile}
      />
    </Switch>
  );
};

export default AppRoutes;
