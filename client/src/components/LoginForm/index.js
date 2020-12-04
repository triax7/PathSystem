import { observer } from 'mobx-react'
import React from 'react'
import Service from './service'
import { Field, Form, Formik } from 'formik'
import { TextField } from 'formik-material-ui'
import Box from '@material-ui/core/Box'
import Paper from '@material-ui/core/Paper'
import Grid from '@material-ui/core/Grid'
import Button from '@material-ui/core/Button'

const LoginForm = observer(
  class extends React.Component {
    service

    constructor(props) {
      super(props)
      this.service = new Service()
    }

    render() {
      const {initialValues} = this.service.state
      const {
        handleSubmit,
        validateEmail,
      } = this.service

      return (
        <Box mt={6}>
          <Grid container justify={'center'}>
            <Grid item xs={3}>
              <Paper>
                <Box p={2}>
                  <Formik initialValues={initialValues}
                          onSubmit={handleSubmit}
                  >
                    <Form>
                      <Grid container direction={'column'}
                            alignItems={'center'}>
                        <Field
                          component={TextField}
                          fullWidth
                          name={'email'}
                          label={'Email'}
                          validate={validateEmail}
                        />
                        <Field
                          component={TextField}
                          fullWidth
                          name={'password'}
                          label={'Password'}
                        />
                      </Grid>
                      <Button type={'submit'} fullWidth
                              variant={'contained'}>Login</Button>
                    </Form>
                  </Formik>
                </Box>
              </Paper>
            </Grid>
          </Grid>
        </Box>
      )
    }
  }
)

export default LoginForm
