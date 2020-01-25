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
import AccountCircle from '@material-ui/icons/AccountCircle'
import ExitToAppIcon from '@material-ui/icons/ExitToApp';
import SettingsContainer from './../modules/Settings/SettingsContainer';
import primaryColor from './../../color';


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
    padding: theme.spacing(1),
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
    <div className="dashboard-container">
      <CssBaseline />
      <AppBar
        style={{ background: primaryColor }}
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
          <NavLink to="/" className="logo">

            <Typography variant="button" className="title">
              e-dziennik
          </Typography>
          </NavLink>
          <IconButton className="icon-button" color="inherit">
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
          <h2 style={{ marginLeft: '5%', marginRight: 'auto' }}>
            {props.user.firstname
              && props.user.surname
              && (`${props.user.firstname} ${props.user.surname}`)}
          </h2>

          <IconButton onClick={handleDrawerClose}>
            {theme.direction === 'ltr' ? <ChevronLeftIcon /> : <ChevronRightIcon />}
          </IconButton>
        </div>
        <Divider />
        <List >
          <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons" />
          {props.menu.map(item => {
            return (
              <NavLink
                key={item.to}
                to={item.to}
                className="inactive-button"
                activeClassName="active-button"
              >
                <ListItem button key={item.key}>
                  <ListItemIcon key={item.icon}>  <Icon> {item.icon} </Icon> </ListItemIcon>
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
          <Route path="/timetable" exact component={props.components.TimetableContainer} />
          <Route path="/grades" exact component={props.components.GradesContainer} />
          <Route path="/presence" exact component={props.components.PresenceContainer} />
          <Route path="/notes" exact component={props.components.NotesContainer} />
          <Route path="/me" exact component={SettingsContainer} />
          <Route path="/subjects" exact component={props.components.SubjectsContainer} />
          <Route path="/subjects/:subjectId" component={props.components.SubjectTeacherContainer} />
          <Route path="/lessons/" exact component={props.components.LessonsContainer} />
          <Route path="/lessons/:classId/:day/:lessonId" component={props.components.AddLessonContainer} />
          <Route path="/users/" exact component={props.components.UsersContainer} />
          <Route path="/users/:userId" exact component={props.components.AddUserContainer} />
          <Route path="/classes/" exact component={props.components.ClassesContainer} />
          <Route path="/classes/:classId" component={props.components.AddStudentsToClassContainer} />
        </div>
      </main>
    </div >
  );
}

export default Layout;
