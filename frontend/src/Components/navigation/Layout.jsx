import React from 'react';
import clsx from 'clsx';
import { makeStyles, useTheme } from '@material-ui/core/styles';
import Drawer from '@material-ui/core/Drawer';
import CssBaseline from '@material-ui/core/CssBaseline';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import List from '@material-ui/core/List';
import Typography from '@material-ui/core/Typography';
import Divider from '@material-ui/core/Divider';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';
import ChevronLeftIcon from '@material-ui/icons/ChevronLeft';
import ChevronRightIcon from '@material-ui/icons/ChevronRight';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import Icon from '@material-ui/core/Icon';
import { NavLink, Route } from 'react-router-dom'
import TimetableContainer from './../modules/Student/Timetable/TimetableContainer';
import GradesContainer from './../modules/Student/Grades/GradesContainer';
import AccountCircle from '@material-ui/icons/AccountCircle'
import ExitToAppIcon from '@material-ui/icons/ExitToApp';
import SettingsContainer from './../modules/Settings/SettingsContainer';
import PresenceContainer from './../modules/Student/PresenceContainer';
import styles from './../../layout';


const drawerWidth = 240;

const useStyles = makeStyles(theme => ({
  root: {
    display: 'flex',
  },
  appBar: {
    transition: theme.transitions.create(['margin', 'width'], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen,
    }),
  },
  appBarShift: {
    width: `calc(100% - ${drawerWidth}px)`,
    marginLeft: drawerWidth,
    transition: theme.transitions.create(['margin', 'width'], {
      easing: theme.transitions.easing.easeOut,
      duration: theme.transitions.duration.enteringScreen,
    }),
  },
  menuButton: {
    marginRight: theme.spacing(2),
  },
  hide: {
    display: 'none',
  },
  drawer: {
    width: drawerWidth,
    flexShrink: 0,
  },
  drawerPaper: {
    width: drawerWidth,
  },
  drawerHeader: {
    display: 'flex',
    alignItems: 'center',
    padding: theme.spacing(0, 1),
    ...theme.mixins.toolbar,
    justifyContent: 'flex-end',
  },
  content: {
    flexGrow: 1,
    padding: theme.spacing(3),
    transition: theme.transitions.create('margin', {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen,
    }),
    marginLeft: -drawerWidth,
  },
  contentShift: {
    transition: theme.transitions.create('margin', {
      easing: theme.transitions.easing.easeOut,
      duration: theme.transitions.duration.enteringScreen,
    }),
    marginLeft: 0,
  },
  inactiveButton: {
    textDecoration: 'none',
    color: 'black',
  },
  activeButton: {
    textDecoration: 'none',
    color: 'blue',
  },
  logo: {
    textDecoration: 'none',
    color: 'white',
  },
  title: {
    flexGrow: 1,
  }
}));

const Layout = (props) => {
  const classes = useStyles();
  const theme = useTheme();
  const [open, setOpen] = React.useState(true);

  const handleDrawerOpen = () => {
    setOpen(true);
  }

  const handleDrawerClose = () => {
    setOpen(false);
  }

  return (
    <div className={classes.root}>
      <CssBaseline />
      <AppBar
        style={{ background: styles.COLORS.primary }}
        position="fixed"
        className={clsx(classes.appBar, {
          [classes.appBarShift]: open,
        })}
      >
        <Toolbar>
          <IconButton
            color="inherit"
            aria-label="open drawer"
            onClick={handleDrawerOpen}
            edge="start"
            className={clsx(classes.menuButton, open && classes.hide)}
          >
            <MenuIcon />
          </IconButton>
          <NavLink to="/" className={classes.logo}>

            <Typography variant="button" style={{ fontSize: "150%" }} className={classes.title}>
              e-dziennik
          </Typography>
          </NavLink>
          <IconButton style={{ marginRight: '0%', marginLeft: 'auto' }} color="inherit">
            <NavLink to="/me" style={{ color: 'inherit' }}>
              <AccountCircle />
            </NavLink>
          </IconButton>
          <IconButton color="inherit" onClick={props.logout}>
              <ExitToAppIcon />
          </IconButton>




        </Toolbar>
      </AppBar>
      <Drawer
        className={classes.drawer}
        variant="persistent"
        anchor="left"
        open={open}
        classes={{
          paper: classes.drawerPaper,
        }}
      >
        <div className={classes.drawerHeader}>
          <IconButton onClick={handleDrawerClose}>
            {theme.direction === 'ltr' ? <ChevronLeftIcon /> : <ChevronRightIcon />}
          </IconButton>
        </div>
        <Divider />
        <List>
          <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons" />
          {props.menu.map(item => {
            return (
              <NavLink
                to={item.to}
                className={classes.inactiveButton}
                activeClassName={classes.activeButton}
              >
                <ListItem button key={item.key}>
                  <ListItemIcon>   <Icon> {item.icon} </Icon> </ListItemIcon>
                  <ListItemText primary={item.name} />
                </ListItem>
              </NavLink>
            );
          })}

        </List>
      </Drawer>
      <main
        className={clsx(classes.content, {
          [classes.contentShift]: open,
        })}
      >
        <div className={classes.drawerHeader} />
        <div className="content">
          <Route path="/timetable" exact component={TimetableContainer} />
          <Route path="/grades" exact component={GradesContainer} />
          <Route path="/presence" exact component={PresenceContainer} />
          <Route path="/me" exact component={SettingsContainer} />
        </div>
      </main>
    </div >
  );
}

export default Layout;
