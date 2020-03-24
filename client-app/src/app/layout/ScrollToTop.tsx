import { useEffect } from "react";
import { useLocation, withRouter } from "react-router-dom";

const ScrollToTop = ({ children, pathname }: any) => {
  useEffect(() => {
    window.scrollTo(0, 0);
  }, [pathname]);

  return children;
};

export default withRouter(ScrollToTop);
