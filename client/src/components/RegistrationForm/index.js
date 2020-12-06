import { observer } from 'mobx-react'
import React from 'react'
import Service from './service'
import { Field, Form, Formik } from 'formik'
import { TextField } from 'formik-material-ui'
import Box from '@material-ui/core/Box'
import Paper from '@material-ui/core/Paper'
import Grid from '@material-ui/core/Grid'
import Button from '@material-ui/core/Button'

const RegistrationForm = observer(
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
        validateName,
        validateEmail,
        validatePassword
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
                          name={'name'}
                          label={'Name'}
                          validate={validateName}
                        />
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
                          validate={validatePassword}
                        />
                      </Grid>
                      <Box mt={2}>
                        <Button type={'submit'} fullWidth variant={'contained'}>
                          Register
                        </Button>
                      </Box>
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

export default RegistrationForm
